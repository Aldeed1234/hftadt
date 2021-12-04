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
        [Table("Planets")]
        public class Planets
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int PlanetId { get; set; }

            [Required]
            [MaxLength(30)]
            public string PlanetName { get; set; }
            public int Difficulty { get; set; }

            [NotMapped]
            [JsonIgnore]
            public virtual ICollection<Players> Players { get; set; }
        }
}

