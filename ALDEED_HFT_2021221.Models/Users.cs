using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Data
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey(nameof(Tweet))]
        public int TweetId { get; set; }

        [Required]
        public string UserName { get; set; }

        public int FollowerCount { get; set; }

        public string Email { get; set; }


        [NotMapped]
        public virtual Tweet Tweet { get; set; }
    }
}
