﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public interface ICommentRepository : IRepository<CommentsRepository>
    {
        void ChangeContent(int id, string newContent);
    }
}
