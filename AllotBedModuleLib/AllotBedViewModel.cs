
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.Serialization;
using System.Windows.Forms;
using MVVMUtilityLib;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using EnableVitalsClass;
using VitalSigns;
using VitalsTypeLib;
using MessageBox = System.Windows.MessageBox;

namespace AllotBedModuleLib
{
    public partial class AllotBedViewModel : INotifyPropertyChanged
    {
        public AllotBedViewModel()
        {
            SubmitCommand = new DelegateCommand((object obj) => { this.AllotBed(PatientId,set); },
                (object obj) => { return true; });
            Listcommand1= new DelegateCommand((object obj) => { this.EnableVitals1(); },
                (object obj) => { return true; });
            Listcommand2 = new DelegateCommand((object obj) => { this.EnableVitals2(); },
                (object obj) => { return true; });
            Listcommand3 = new DelegateCommand((object obj) => { this.EnableVitals3(); },
               (object obj) => { return true; });

            set.VitalsSigns = new List<VitalSign>();


        }

    
        private string _patientId;

       

        readonly SetVitals set = new SetVitals();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { get; set; }
        public ICommand Listcommand1 { get; set; }
        public ICommand Listcommand2 { get; set; }
        public ICommand Listcommand3 { get; set; }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

      
        public string PatientId
        {
            get { return _patientId; }
            set
            {
                _patientId = value;
                OnPropertyChanged(PatientId);
            }
        }

        private bool _check;
        public bool Checkbox1
        {
            get { return _check; }

            set
            {
                _check = value;
                OnPropertyChanged("Checkbox1");
            }
        }

        public bool Checkbox2
        {
            get { return _check; }

            set
            {
                _check = value;
                OnPropertyChanged("Checkbox2");
            }
        }


        public bool Checkbox3
        {
            get { return _check; }

            set
            {
                _check = value;
                OnPropertyChanged("Checkbox3");
            }
        }

        private void SendMessage(string parameter)
        {
            
            PatientId = "";
            Checkbox1 = false;
            Checkbox2 = false;
            Checkbox3 = false;
        }

        public void EnableVitals1()
        {
            set.PatientId = PatientId;
        
            set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = 0 });
        }
        public void EnableVitals2()
        {
            set.PatientId = PatientId;
            set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = VitalsTypeLib.VitalsType.PulseRate});
        }
        public void EnableVitals3()
        {
            set.PatientId = PatientId;

            set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = VitalsTypeLib.VitalsType.Temperature });
        }


        public void Check()
        {
            if(set.PatientId==null)
            {
                set.PatientId = PatientId;

                set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = 0 });
                set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = VitalsTypeLib.VitalsType.PulseRate });
                set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = VitalsTypeLib.VitalsType.Temperature });

            }
        }
        public bool AllotBed(string PatientId,SetVitals set)
        {


            Check();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
            string BedConfigureURI = "http://localhost:58905/api/BedConfiguration/";
            var responseMessage = client.PostAsJsonAsync
                (BedConfigureURI+"AllotBed", PatientId).Result;
            bool x = responseMessage.IsSuccessStatusCode;
            string PatientMonitoriURI = "http://localhost:1080/api/PatientMonitoring/";
            var httpResponse=
                client.PostAsJsonAsync
                (PatientMonitoriURI+"/EnablePatientVitals",
                set).Result;
          if(httpResponse.IsSuccessStatusCode)
              SendMessage("");
        
            x = x && (httpResponse.IsSuccessStatusCode);
           
            return x;


        }
    }
}

