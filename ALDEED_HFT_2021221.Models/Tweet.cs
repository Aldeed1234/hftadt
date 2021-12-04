using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Data
{
    [Table("Tweets")]
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }

        [Required]
        [MaxLength(280)]
        public string TweetContent { get; set; }

        public string UserName { get; set; }

        [NotMapped]
        public string AllData => $"[{TweetId}] : {UserName} : {TweetContent} (like: {LikesCount}) (retweets: {RetweetCount}) (comments: {Comments.Count()})";

        public int? LikesCount { get; set; }
        
        public int? RetweetCount { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public object User { get; internal set; }

        public Tweet()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
