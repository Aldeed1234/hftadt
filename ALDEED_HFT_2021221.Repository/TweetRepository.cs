using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALDEED_HFT_2021221.Data;
using Microsoft.EntityFrameworkCore;

namespace ALDEED_HFT_2021221.Repository
{
    public class TweetRepository : Repository<Tweet>, ITweetRepository
    {
        public TweetRepository(DbContext ctx) : base(ctx)
        {

        }
        public void ChangeContent(int id, string newContent)
        {
            var tweet = this.GetOne(id);
            if (tweet == null)
            {
                throw new InvalidOperationException("tweet not found");
            }
            tweet.TweetContent = newContent;
            this.Ctx.Savechanges();
        }

        public override Tweet GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }

        public override void DeleteComment(int id)
        {
            Comment obj = this.GetOne(id);
            this.Ctx.Set<Comment>().Remove(obj);
            this.Ctx.SaveChanges();
        }

        public override void Remove(int id)
        {
            Tweet obj = this.GetOne(id);
            this.Ctx.Set<Tweet>().Remove(obj);
            this.Ctx.SaveChanges();
        }

        public override void Insert(Tweet entity)
        {
            this.Ctx.Set<Tweet>().Add(entity);
            this.Ctx.SaveChanges();
        }
    }

    
}
