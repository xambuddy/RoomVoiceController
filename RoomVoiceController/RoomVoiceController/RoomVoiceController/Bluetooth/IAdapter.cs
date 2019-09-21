using System;
using System.Collections.Generic;
using System.Text;

namespace RoomVoiceControl.Bluetooth
{
    public interface IAdapter
    {
        List<BluetoothDevice> GetBluetoothDevices();

        IBluetoothSocket Connect(BluetoothDevice device);

        void CheckBluetooth();

        void Disconnect();
    }
}
