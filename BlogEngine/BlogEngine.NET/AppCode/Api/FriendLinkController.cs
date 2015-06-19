using BlogEngine.Core.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BlogEngine.NET.AppCode.Api
{
    public class FriendLinkController : ApiController
    {
        readonly IFriendLinkRepository repository;
        public FriendLinkController(IFriendLinkRepository repository)
        {
            this.repository = repository;
        }
    }
}