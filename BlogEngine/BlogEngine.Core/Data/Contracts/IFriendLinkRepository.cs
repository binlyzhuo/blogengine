using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Contracts
{
    public interface IFriendLinkRepository
    {
        IEnumerable<Data.Models.FriendLinkItem> Find(int take = 10, int skip = 0, string filter = "", string order = "");
        Data.Models.FriendLinkItem Add(Data.Models.FriendLinkItem item);
    }
}
