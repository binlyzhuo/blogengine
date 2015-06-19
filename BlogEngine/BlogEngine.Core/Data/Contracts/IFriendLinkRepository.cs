using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Contracts
{
    /// <summary>
    /// friendlink repository interface
    /// </summary>
    public interface IFriendLinkRepository
    {
        IEnumerable<Data.Models.FriendLinkItem> Find(int take = 10, int skip = 0, string filter = "", string order = "");

        /// <summary>
        /// add friendlink
        /// </summary>
        /// <param name="item">item</param>
        /// <returns></returns>
        Data.Models.FriendLinkItem Add(Data.Models.FriendLinkItem item);

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>True on success</returns>
        bool Remove(Guid id);
    }
}
