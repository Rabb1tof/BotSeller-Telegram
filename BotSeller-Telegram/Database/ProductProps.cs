using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotSeller_Telegram.Database
{
    public class ProductProps
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public UserProps Owner { get; set; }
        public string Price { get; set; }
    }
}
