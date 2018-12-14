using LiveChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.ViewModels
{
    public class ViewModel
    {
        public List<Notification> Notifications { get; set; }
        public List<LiveChat.Models.Task> Tasks { get; set; }
    }
}
