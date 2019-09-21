using Acr.UserDialogs;
using RoomVoiceControl.Interfaces;
using RoomVoiceControl.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoomVoiceControl
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ISpeechToText _speechRecongnitionInstance;
        public MainPage()
        {
            InitializeComponent();

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
                StartBtn.IsEnabled = true;
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            MessageLabel.Text = args;
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                StartBtn.IsEnabled = false;
            }
        }
    }
}
