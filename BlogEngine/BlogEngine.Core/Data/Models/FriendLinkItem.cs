using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Models
{
    public class FriendLinkItem
    {
        public string Name { set; get; }
        public string Url { set; get; }
        public string Keywords { set; get; }
        public string LinkMan { set; get; }

        public bool IsChecked { get; set; }

        public Guid ID { get; set; }
    }
}
