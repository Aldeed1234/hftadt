using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Models
{
    [Table("Seasons")]
    public class PvpEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PvpEventId { get; set; }

        [MaxLength(50)]
        public string PvpEventName { get; set; }

        [ForeignKey(nameof(Locations))]
        public int LocationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Locations Location { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Players> Players { get; set; }
    }
}
