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
            if(args.ToLower().Contains("main light on"))
            {
                MainLightStatus = true;
            }
            if (args.ToLower().Contains("main light off"))
            {
                MainLightStatus = false;
            }
        }

        public ICommand VoiceCommand { get; set; }

        public void SendData()
        {
            string data = "#$";

            if (MainLightStatus)
                data += "1";
            else
                data += "0";

            data += "1111";

            data += "%";

            Shell.BTSocket?.WriteData(data);
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
                SendData();
            }
        }
    }
}
