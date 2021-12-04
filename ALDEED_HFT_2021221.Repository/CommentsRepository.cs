using ALDEED_HFT_2021221.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public class CommentsRepository : Repository<Comment>, ICommentRepository
    {
        public CommentsRepository(DbContext ctx) : base(ctx)
        {

        }
        public void ChangeContent(int id, string newContent)
        {
            var comment = this.GetOne(id);
            if (comment == null)
            {
                throw new InvalidOperationException("comment not found");
            }
            comment.Content = newContent;
            this.Ctx.Savechanges();
        }

        public void ReTweet(int id, string Content, Comment entity)
        {
            this.Ctx.Set<Comment>().Add(entity);
            this.Ctx.SaveChanges();
        }
    
        public override Comment GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Remove(int id)
        {
            Comment obj = this.GetOne(id);
            this.Ctx.Set<Comment>().Remove(obj);
            this.Ctx.SaveChanges();
        }

        public override void Insert(Comment entity)
        {
            this.Ctx.Set<Comment>().Add(entity);
            this.Ctx.SaveChanges();
        }

    }





}
