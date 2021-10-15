using System;
using System.Collections.Generic;

#nullable disable

namespace EndpointAPI.Model
{
    public partial class Artist
    {
        public int ArtistId { get; set; }
        public DateTime DateCreation { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public string ImageUrl { get; set; }
        public string HeroUrl { get; set; }
    }
}
