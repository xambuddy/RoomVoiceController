using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using RoomVoiceControl.Bluetooth;

[assembly: Xamarin.Forms.Dependency(typeof(Adapter))]
namespace RoomVoiceController.Droid.Bluetooth
{
    public class Adapter : RoomVoiceControl.Bluetooth.IAdapter
    {
        private static string MY_UUID = "00001101-0000-1000-8000-00805F9B34FB";
        private BluetoothAdapter mBluetoothAdapter = null;
        private BluetoothSocket btSocket = null;
        private Stream outStream = null;

        public void CheckBluetooth()
        {
            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (!mBluetoothAdapter.Enable())
            {
                UserDialogs.Instance.Alert("Bluetooth Deactivated!");
            }
            if (mBluetoothAdapter == null)
            {
                UserDialogs.Instance.Alert("Bluetooth Not Existing!");
            }
        }

        public IBluetoothSocket Connect(RoomVoiceControl.Bluetooth.BluetoothDevice device)
        {
            IBluetoothSocket socket = null;
            Android.Bluetooth.BluetoothDevice bluetoothDevice = mBluetoothAdapter.GetRemoteDevice(device.Address);
            UserDialogs.Instance.Toast("Connecting to " + bluetoothDevice);

            try
            {
                btSocket = bluetoothDevice.CreateRfcommSocketToServiceRecord(UUID.FromString(MY_UUID));

                btSocket.Connect();

                socket = new AndroidBluetoothSocket() { BTSocket = btSocket };

                UserDialogs.Instance.Toast("Connected to Bluetooth!");
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    btSocket.Close();
                }
                catch (System.Exception)
                {
                    UserDialogs.Instance.Alert("Connection Error!");
                }
            }

            return socket;
        }

        public void Disconnect()
        {
            if (btSocket.IsConnected)
            {
                try
                {
                    btSocket.Close();
                }
                catch (System.Exception ex)
                {
                    UserDialogs.Instance.Alert("Error!");
                }
            }
        }

        public List<RoomVoiceControl.Bluetooth.BluetoothDevice> GetBluetoothDevices()
        {
            mBluetoothAdapter.StartDiscovery();

            List<RoomVoiceControl.Bluetooth.BluetoothDevice> list = new List<RoomVoiceControl.Bluetooth.BluetoothDevice>();

            try
            {
                var devices = mBluetoothAdapter.BondedDevices;
                foreach (var device in devices)
                {
                    list.Add(new RoomVoiceControl.Bluetooth.BluetoothDevice() { Name = device.Name, Address = device.Address });
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mBluetoothAdapter.CancelDiscovery();
            }

            return list;
        }

        public void Setup()
        {
            throw new NotImplementedException();
        }

        public void WriteData(string data)
        {
            try
            {
                outStream = btSocket.OutputStream;
            }
            catch (System.Exception e)
            {
                UserDialogs.Instance.Alert("Error Sending Data!");
            }

            Java.Lang.String message = new Java.Lang.String(data.ToCharArray());

            byte[] msgBuffer = message.GetBytes();

            try
            {
                outStream.Write(msgBuffer, 0, msgBuffer.Length);
            }
            catch (System.Exception e)
            {
                UserDialogs.Instance.Alert("Error Sending Data!");
            }
        }
    }

    public class AndroidBluetoothSocket : IBluetoothSocket
    {
        private Stream outStream = null;
        public BluetoothSocket BTSocket { get; set; }
        public void WriteData(string data)
        {
            if (BTSocket == null)
                return;

            try
            {
                outStream = BTSocket.OutputStream;
            }
            catch (System.Exception e)
            {
                UserDialogs.Instance.Alert("Error Sending Data!");
            }

            Java.Lang.String message = new Java.Lang.String(data.ToCharArray());

            byte[] msgBuffer = message.GetBytes();

            try
            {
                outStream.Write(msgBuffer, 0, msgBuffer.Length);
            }
            catch (System.Exception e)
            {
                UserDialogs.Instance.Alert("Error Sending Data!");
            }
        }
    }
}