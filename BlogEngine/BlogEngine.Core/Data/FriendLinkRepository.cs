using BlogEngine.Core.Data.Contracts;
using BlogEngine.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data
{
    public class FriendLinkRepository : IFriendLinkRepository
    {
        public IEnumerable<Data.Models.FriendLinkItem> Find(int take = 10, int skip = 0, string filter = "", string order = "")
        {
            var items = FriendLink.FriendLinks.Select(u =>
            {
                return new FriendLinkItem()
                {
                    Contact2 = u.Contact,
                    Keywords = u.KeyWords,
                    Name = u.Name,
                    LinkGuid = u.Id,
                    Url = u.Url
                };
            });
            return items;
        }

        public Data.Models.FriendLinkItem Add(Data.Models.FriendLinkItem item)
        {
            if (!Security.IsAuthorizedTo(BlogEngine.Core.Rights.CreateNewPosts))
                throw new System.UnauthorizedAccessException();

            var cat = (from c in FriendLink.FriendLinks.ToList() where c.Name == item.Name select c).FirstOrDefault();
            if (cat != null)
                throw new ApplicationException("Name must be unique");

            try
            {
                var newItem = new FriendLink(item.Name, item.Url, item.Keywords, item.Contact2,Guid.NewGuid());


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
