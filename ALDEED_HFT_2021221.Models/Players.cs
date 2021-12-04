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
        public class Players
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int PlayerId { get; set; }

            [Required]
            [MaxLength(50)]
            public string PlayerName { get; set; }

            public int PlayerLevel { get; set; }

            public bool IsEliminated { get; set; }

            [ForeignKey(nameof(Planets))]
            public int? EliminatedOnPlanet_PlanetId { get; set; }

            [ForeignKey(nameof(PvpEvent))]
            public int PvpEventId { get; set; }

            [NotMapped]
            [JsonIgnore]
            public virtual PvpEvent PvpEvent { get; set; }

            [NotMapped]
            [JsonIgnore]
            public virtual Planets Planets { get; set; }

            public Players()
            {
                IsEliminated = true;
            }
        }
}
