using Acr.UserDialogs;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using RoomVoiceControl.Bluetooth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RoomVoiceController.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        RoomVoiceControl.Bluetooth.IAdapter _Adapter;
        public SettingsViewModel()
        {
            _Adapter = DependencyService.Get<RoomVoiceControl.Bluetooth.IAdapter>();
            _Adapter.CheckBluetooth();
            DeviceList = new ObservableCollection<BluetoothDevice>();
            this.CommandScan = new Command(CommandScanExecute);
            this.CommandConnect = new Command(CommandConnectExecute);
        }

        public ICommand CommandScan { get; protected set; }
        public ICommand CommandConnect { get; protected set; }

        private ObservableCollection<BluetoothDevice> _DeviceList;
        public ObservableCollection<BluetoothDevice> DeviceList
        {
            get
            {
                return _DeviceList;
            }
            set
            {
                _DeviceList = value;
                OnPropertyChanged(nameof(DeviceList));
            }
        }

        private BluetoothDevice _Device;
        public BluetoothDevice Device
        {
            get
            {
                return _Device;
            }
            set
            {
                _Device = value;
                OnPropertyChanged(nameof(Device));
                OnPropertyChanged(nameof(ConnectEnabled));
            }
        }

        public bool ConnectEnabled
        {
            get
            {
                return Device != null;
            }
        }

        private void CommandScanExecute()
        {
            try
            {
                DeviceList.Clear();

                DeviceList = new ObservableCollection<BluetoothDevice>(_Adapter.GetBluetoothDevices());
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert("Error in fetching devices!");
            }
        }

        private void CommandConnectExecute()
        {
            try
            {
                if (Device != null)
                {
                    var socket = _Adapter.Connect(Device);

                    Shell.BTSocket = socket;
                }
                else
                {
                    UserDialogs.Instance.Alert("Please select a device!");
                }
            }
            catch (DeviceConnectionException ex)
            {
                UserDialogs.Instance.Alert(ex.Message.ToString());
            }
        }
    }
}
