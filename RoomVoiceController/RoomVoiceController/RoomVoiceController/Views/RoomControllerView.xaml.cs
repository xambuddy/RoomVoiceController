using RoomVoiceController.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomVoiceController.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomControllerView : ContentPage
    {
        public RoomControllerView()
        {
            InitializeComponent();
            BindingContext = new RoomControllerViewModel();
        }
    }
}