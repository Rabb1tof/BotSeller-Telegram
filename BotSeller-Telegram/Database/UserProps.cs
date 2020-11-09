using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotSeller_Telegram.Database
{
    public class UserProps
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public long ProductCount { get; set; }
    }
}
