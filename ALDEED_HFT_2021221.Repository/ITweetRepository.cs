using ALDEED_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALDEED_HFT_2021221.Repository
{
    public interface ITweetRepository : IRepository<TweetRepository>
    {
        void ChangeContent(int id, string newContent);

        void DeleteComment(int id);


    }
}
