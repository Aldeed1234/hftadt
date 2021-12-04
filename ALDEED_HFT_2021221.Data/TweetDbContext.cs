using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Data
{
    public class TweetDbContext : DbContext
    {
        public virtual DbSet<Tweet> Tweet { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public TweetDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Tweet t1 = new Tweet() { UserName = "introwertyyzm", TweetContent = "I had the best day of my life", LikesCount = 32, RetweetCount = 12, };
            Tweet t2 = new Tweet() { UserName = "asdsasdsa", TweetContent = "I had the worst day of my life", LikesCount = 12, RetweetCount = 22, };
            Tweet t3 = new Tweet() { UserName = "dojacat", TweetContent = "concert on in 10 minutes", LikesCount = 32000, RetweetCount = 22000, };
            Tweet t4 = new Tweet() { UserName = "kasia", TweetContent = "doja cat good", LikesCount = 11000, RetweetCount = 3000, };
            Tweet t5 = new Tweet() { UserName = "pancakeman", TweetContent = "pancake: GOOD", LikesCount = 100000, RetweetCount = 60000, };

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(comment => comment.Tweet)
                .WithMany(tweet => tweet.Comments)
                .HasForeignKey(comment => comment.Tweet)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //modelBuilder.Entity<Users>(entity =>
            //{
            //    entity.HasOne(user => user.Tweet)
            //    .WithMany(tweet=>tweet.User)
            //});

            modelBuilder.Entity<Tweet>().HasData(t1, t2, t3, t4, t5);
        }
        
    }
}
