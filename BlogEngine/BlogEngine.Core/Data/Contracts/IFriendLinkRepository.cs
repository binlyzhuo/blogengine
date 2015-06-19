using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Contracts
{
    public interface IFriendLinkRepository
    {
        Data.Models.FriendLink Add(Data.Models.FriendLink item);
    }
}
