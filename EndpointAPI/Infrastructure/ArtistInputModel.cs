using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EndpointAPI.Infrastructure
{
    public class ArtistInputModel
    {
        [Required]
        public string artistName { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string HeroUrl { get; set; }
    }
}
