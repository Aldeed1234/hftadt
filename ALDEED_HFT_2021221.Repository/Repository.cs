using ALDEED_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected VideoGameDbContext videoGameDbContext;

        public Repository(VideoGameDbContext videoGameDbContext)
        {
            this.videoGameDbContext = videoGameDbContext;
        }

        public IQueryable<T> GetAll()
        {
            return videoGameDbContext.Set<T>();
        }

        public abstract T GetOne(int id);

        public void Add(T entity)
        {
            videoGameDbContext.Set<T>().Add(entity);
            videoGameDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            videoGameDbContext.Set<T>().Remove(entity);
            videoGameDbContext.SaveChanges();
        }
    }
}
