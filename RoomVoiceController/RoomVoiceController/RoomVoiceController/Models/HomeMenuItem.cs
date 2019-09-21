using System;
using System.Collections.Generic;
using System.Text;

namespace RoomVoiceController.Models
{
    public enum MenuItemType
    {
        RoomController,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
