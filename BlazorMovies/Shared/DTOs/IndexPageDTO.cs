using BlazorMovies.Shared.Entities;
using System.Collections.Generic;

namespace BlazorMovies.Shared.DTOs
{
    public class IndexPageDTO
    {
        public List<Movie> Intheaters { get; set; }
        public List<Movie> UpcomingReleases { get; set; }
    }
}
