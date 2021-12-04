using ALDEED_HFT_2021221.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(DbContext ctx) : base(ctx)
        {

        }

        public void ChangeUserName(int id, string newUserName)
        {
            var user = this.GetOne(id);
            if (user == null)
            {
                throw new InvalidOperationException("Customer not found!");
            }

            user.UserName = newUserName;
            this.Ctx.SaveChanges();
        }

        public void ChangeEmail(int id, string newEmail)
        {
            var user = this.GetOne(id);
            if (user == null)
            {
                throw new InvalidOperationException("Customer not found!");
            }

            user.Email = newEmail;
            this.Ctx.SaveChanges();
        }

        public override Users GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }

        public override void Remove(int id)
        {
            Users obj = this.GetOne(id);
            this.Ctx.Set<Users>().Remove(obj);
            this.Ctx.SaveChanges();
        }

        public override void Insert(Users entity)
        {
            this.Ctx.Set<Users>().Add(entity);
            this.Ctx.SaveChanges();
        }
    }
}
