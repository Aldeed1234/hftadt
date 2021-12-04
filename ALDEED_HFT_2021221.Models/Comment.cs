using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Data
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [MaxLength(280)]
        public string Content { get; set; }

        [NotMapped]
        public virtual Tweet Tweet { get; set; }

        [ForeignKey(nameof(Tweet))]
        public int TweetId { get; set; }
    }
}
