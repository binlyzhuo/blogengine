using BlogEngine.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core
{
    /// <summary>
    /// 友情链接相关
    /// </summary>
    public class FriendLink : BusinessBase<FriendLink, Guid>
    {
        #region Constants and Fields

        /// <summary>
        ///     The sync root.
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        ///     The friendlinks.
        /// </summary>
        private static Dictionary<Guid, List<FriendLink>> friendlinks = new Dictionary<Guid, List<FriendLink>>();

        /// <summary>
        ///     The description.
        /// </summary>
        private string name;

        private string url;

        private string keywords;

        private string contact;

        private DateTime addDate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Category"/> class. 
        /// </summary>
        static FriendLink()
        {
            Blog.Saved += (s, e) =>
            {
                if (e.Action == SaveAction.Delete)
                {
                    Blog blog = s as Blog;
                    if (blog != null)
                    {
                        // remove deleted blog from static 'friendlinks'

                        if (friendlinks != null && friendlinks.ContainsKey(blog.Id))
                            friendlinks.Remove(blog.Id);
                    }
                }
            };
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref = "Category" /> class.
        /// </summary>
        public FriendLink()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// contruct function
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="keywords"></param>
        /// <param name="contact"></param>
        /// <param name="guid"></param>
        public FriendLink(string name, string url,string keywords,string contact,Guid guid)
        {
            this.Id = guid;
            this.name = name;
            this.url = url;
            this.keywords = keywords;
            this.contact = contact;
            
            
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a sorted list of all Categories.
        /// </summary>
        /// <value>The categories.</value>
        public static List<FriendLink> FriendLinks
        {
            get
            {
                Blog blog = Blog.CurrentInstance;
                List<FriendLink> blogFriendLinks;

                if (!friendlinks.TryGetValue(blog.BlogId, out blogFriendLinks))
                {
                    lock (SyncRoot)
                    {
                        if (!friendlinks.TryGetValue(blog.BlogId, out blogFriendLinks))
                        {
                            friendlinks[blog.Id] = blogFriendLinks = BlogService.FillFriendLinks(blog);

                            if (blogFriendLinks != null)
                                blogFriendLinks.Sort();
                        }
                    }
                }
                return blogFriendLinks;
            }
        }

        /// <summary>
        ///     Gets a sorted list of all Categories across all blog instances.
        /// </summary>
        /// <value>The categories.</value>
        public static List<FriendLink> AllBlogFriendLinks
        {
            get
            {
                List<Blog> blogs = Blog.Blogs.Where(b => b.IsActive).ToList();
                Guid originalBlogInstanceIdOverride = Blog.InstanceIdOverride;
                List<FriendLink> friendlinksAcrossAllBlogs = new List<FriendLink>();

                // Categories are not loaded for a blog instance until that blog
                // instance is first accessed.  For blog instances where the
                // categories have not yet been loaded, using InstanceIdOverride to
                // temporarily switch the blog CurrentInstance blog so the Categories
                // for that blog instance can be loaded.
                //
                for (int i = 0; i < blogs.Count; i++)
                {
                    List<FriendLink> blogFriendLinks;
                    if (!friendlinks.TryGetValue(blogs[i].Id, out blogFriendLinks))
                    {
                        // temporarily override the Current BlogId to the
                        // blog Id we need categories to be loaded for.
                        Blog.InstanceIdOverride = blogs[i].Id;
                        blogFriendLinks = FriendLinks;
                        Blog.InstanceIdOverride = originalBlogInstanceIdOverride;
                    }

                    friendlinksAcrossAllBlogs.AddRange(blogFriendLinks);
                }

                return friendlinksAcrossAllBlogs;
            }
        }

        /// <summary>
        ///     Gets a sorted list of all Categories, taking into account the
        ///     current blog instance's Site Aggregation status in determining if
        ///     categories from just the current instance or all instances should
        ///     be returned.
        /// </summary>
        /// <remarks>
        ///     This logic could be put into the normal 'Categories' property, however
        ///     there are times when a Site Aggregation blog instance may just need
        ///     its own categories.  So ApplicableCategories can be called when data
        ///     across all blog instances may be needed, and Categories can be called
        ///     when data for just the current blog instance is needed.
        /// </remarks>
        public static List<FriendLink> ApplicableCategories
        {
            get
            {
                if (Blog.CurrentInstance.IsSiteAggregation)
                    return AllBlogFriendLinks;
                else
                    return FriendLinks;
            }
        }

        
        /// <summary>
        ///     Gets or sets the Description of the object.
        /// </summary>
        /// <value>The description.</value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                base.SetValue("Name", value, ref this.name);
            }
        }

        public DateTime AddDate
        {
            get
            {
                return this.addDate;
            }
            set
            {
                base.SetValue("AddDate", value, ref this.addDate);
            }
        }

        /// <summary>
        ///     Gets or sets the Parent ID of the object
        /// </summary>
        /// <value>The parent.</value>
        public string Url
        {
            get
            {
                return this.url;
            }

            set
            {
                base.SetValue("Url", value, ref this.url);
            }
        }

        /// <summary>
        /// site contact
        /// </summary>
        public string Contact
        {
            get
            {
                return this.contact;
            }

            set
            {
                base.SetValue("Contact", value, ref this.contact);
            }
        }

        /// <summary>
        ///     Gets or sets the Title or the object.
        /// </summary>
        /// <value>The title.</value>
        public string KeyWords
        {
            get
            {
                return this.keywords;
            }

            set
            {
                base.SetValue("KeyWords", value, ref this.keywords);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a category based on the specified id.
        /// </summary>
        /// <param name="id">
        /// The category id.
        /// </param>
        /// <returns>
        /// The category.
        /// </returns>
        public static FriendLink GetFriendLink(Guid id)
        {
            return GetFriendLink(id, false);
        }

        /// <summary>
        /// Returns a category based on the specified id.
        /// </summary>
        /// <param name="id">
        /// The category id.
        /// </param>
        /// <param name="acrossAllBlogInstances">
        /// Whether to search across the categories of all blog instances.
        /// </param>
        /// <returns>
        /// The category.
        /// </returns>
        public static FriendLink GetFriendLink(Guid id, bool acrossAllBlogInstances)
        {
            return (acrossAllBlogInstances ? AllBlogFriendLinks : FriendLinks).FirstOrDefault(category => category.Id == id);
        }

        

        

        #endregion

        
        #region Methods

        /// <summary>
        /// Deletes the object from the data store.
        /// </summary>
        protected override void DataDelete()
        {
            if (this.Deleted)
            {
                BlogService.DeleteFriendLink(this);
            }

            if (FriendLinks.Contains(this))
            {
                FriendLinks.Remove(this);
            }

            this.Dispose();
        }

        /// <summary>
        /// Inserts a new object to the data store.
        /// </summary>
        protected override void DataInsert()
        {
            if (this.New)
            {
                BlogService.InsertFriendLink(this);
            }
        }

        /// <summary>
        /// Retrieves the object from the data store and populates it.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the object.
        /// </param>
        /// <returns>
        /// True if the object exists and is being populated successfully
        /// </returns>
        protected override FriendLink DataSelect(Guid id)
        {
            return BlogService.SelectFriendLink(id);
        }

        /// <summary>
        /// Updates the object in its data store.
        /// </summary>
        protected override void DataUpdate()
        {
            if (this.IsChanged)
            {
                BlogService.UpdateFriendLink(this);
            }
        }

        /// <summary>
        /// Reinforces the business rules by adding additional rules to the
        ///     broken rules collection.
        /// </summary>
        protected override void ValidationRules()
        {
            this.AddRule("Name", "Name must be set", string.IsNullOrEmpty(this.Name));
            this.AddRule("URL", "URL must be set", string.IsNullOrEmpty(this.Url));
            this.AddRule("KeyWords", "KeyWords must be set", string.IsNullOrEmpty(this.KeyWords));
            this.AddRule("Contact", "Contact must be set", string.IsNullOrEmpty(this.Contact));
            
        }

        #endregion
    }
}
