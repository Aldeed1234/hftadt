using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public interface IUsersRepository : IRepository<UsersRepository>
    {
        void ChangeUserName(int id, string newUserName);

        void ChangeEmail(int id, string newEmail);
    }
}
