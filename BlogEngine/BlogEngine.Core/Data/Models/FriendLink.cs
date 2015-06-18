using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Models
{
    public class FriendLink
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public string Keywords { set; get; }
        public string Contact { set; get; }
    }
}
