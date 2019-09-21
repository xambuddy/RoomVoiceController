using System;
using System.Collections.Generic;
using System.Text;

namespace RoomVoiceControl.Bluetooth
{
    public interface IBluetoothSocket
    {
        void WriteData(string data);
    }
}
