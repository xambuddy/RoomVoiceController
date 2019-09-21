using RoomVoiceControl.Interfaces;
using RoomVoiceControl.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RoomVoiceController.ViewModels
{
    public class RoomControllerViewModel : BaseViewModel
    {
        private ISpeechToText _speechRecongnitionInstance;
        public RoomControllerViewModel()
        {
            VoiceCommand = new Command(VoiceCommandExecute);

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {

            }

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
          
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
        }

        private void VoiceCommandExecute()
        {
            _speechRecongnitionInstance.StartSpeechToText();
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            if(args.ToLower().Contains("door lock"))
                DoorLockStatus = true;
            if (args.ToLower().Contains("door unlock"))
                DoorLockStatus = false;

            if (args.ToLower().Contains("main light on"))
                MainLightStatus = true;
            if (args.ToLower().Contains("main light off"))
                MainLightStatus = false;

            if (args.ToLower().Contains("aircon on"))
                AirconStatus = true;
            if (args.ToLower().Contains("aircon off"))
                AirconStatus = false;

            if (args.ToLower().Contains("tv on"))
                TelevisionStatus = true;
            if (args.ToLower().Contains("tv off"))
                TelevisionStatus = false;

            if (args.ToLower().Contains("dim light on"))
                DimLightStatus = true;
            if (args.ToLower().Contains("dim light off"))
                DimLightStatus = false;

            if (args.ToLower().Contains("sleep mode"))
            {
                DoorLockStatus = true;
                MainLightStatus = false;
                TelevisionStatus = false;
                AirconStatus = true;
                DimLightStatus = true;
            }

            SendData();
        }

        public ICommand VoiceCommand { get; set; }

        public void SendData()
        {
            string data = "#$";

            if (DoorLockStatus)
                data += "1";
            else
                data += "0";

            if (MainLightStatus)
                data += "1";
            else
                data += "0";

            if (AirconStatus)
                data += "1";
            else
                data += "0";

            if (TelevisionStatus)
                data += "1";
            else
                data += "0";

            if (DimLightStatus)
                data += "1";
            else
                data += "0";

            data += "%";

            Shell.BTSocket?.WriteData(data);
        }

        private bool _DoorLockStatus;
        public bool DoorLockStatus
        {
            get
            {
                return _DoorLockStatus;
            }
            set
            {
                _DoorLockStatus = value;
                OnPropertyChanged(nameof(DoorLockStatus));
            }
        }

        private bool _MainLightStatus;
        public bool MainLightStatus
        {
            get
            {
                return _MainLightStatus;
            }
            set
            {
                _MainLightStatus = value;
                OnPropertyChanged(nameof(MainLightStatus));
            }
        }

        private bool _AirconStatus;
        public bool AirconStatus
        {
            get
            {
                return _AirconStatus;
            }
            set
            {
                _AirconStatus = value;
                OnPropertyChanged(nameof(AirconStatus));
            }
        }

        private bool _TelevisionStatus;
        public bool TelevisionStatus
        {
            get
            {
                return _TelevisionStatus;
            }
            set
            {
                _TelevisionStatus = value;
                OnPropertyChanged(nameof(TelevisionStatus));
            }
        }

        private bool _DimLightStatus;
        public bool DimLightStatus
        {
            get
            {
                return _DimLightStatus;
            }
            set
            {
                _DimLightStatus = value;
                OnPropertyChanged(nameof(DimLightStatus));
            }
        }
    }
}
