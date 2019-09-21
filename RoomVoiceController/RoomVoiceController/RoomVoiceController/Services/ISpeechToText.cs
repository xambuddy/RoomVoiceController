using System;
using System.Collections.Generic;
using System.Text;

namespace RoomVoiceControl.Services
{
    public interface ISpeechToText
    {
        void StartSpeechToText();
        void StopSpeechToText();
    }
}
