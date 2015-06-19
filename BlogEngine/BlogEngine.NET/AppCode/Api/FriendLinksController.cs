using BlogEngine.Core.Data.Contracts;
using BlogEngine.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

public class FriendLinksController : ApiController
{
    readonly IFriendLinkRepository repository;
    public FriendLinksController(IFriendLinkRepository repository)
    {
        this.repository = repository;
    }

    public IEnumerable<FriendLinkItem> Get(int take = 10, int skip = 0, string filter = "", string order = "")
    {
        return repository.Find(take, skip, filter, order);
    }

    public HttpResponseMessage Post([FromBody]FriendLinkItem item)
    {
        var result = repository.Add(item);
        //if (result == null)
        //    return Request.CreateResponse(HttpStatusCode.NotFound);

        return Request.CreateResponse(HttpStatusCode.Created, item);
    }
}