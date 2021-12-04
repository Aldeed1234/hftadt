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
        [Table("Locations")]
        public class Locations
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int LocationId { get; set; }

            [MaxLength()]
            public string LocationName { get; set; }
            [NotMapped]
            [JsonIgnore]
            public virtual ICollection<PvpEvent> PvpEvents { get; set; }
        }
}
