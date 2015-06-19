using BlogEngine.Core.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data
{
    public class FriendLinkRepository : IFriendLinkRepository
    {
        public Data.Models.FriendLink Add(Data.Models.FriendLink item)
        {
            if (!Security.IsAuthorizedTo(BlogEngine.Core.Rights.CreateNewPosts))
                throw new System.UnauthorizedAccessException();

            var cat = (from c in FriendLink.FriendLinks.ToList() where c.Name == item.Name select c).FirstOrDefault();
            if (cat != null)
                throw new ApplicationException("Name must be unique");

            try
            {
                var newItem = new FriendLink(item.Name,item.Url,item.Keywords,item.Contact);


                newItem.Save();
                return item;
            }
            catch (Exception ex)
            {
                Utils.Log(string.Format("CategoryRepository.Add: {0}", ex.Message));
                return null;
            }
        }
    }
}
