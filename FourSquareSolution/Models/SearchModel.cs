using System.Collections.Generic;

namespace FourSquareSolution.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Venues = new List<VenueModel>();
        }

        public string SearchQuery { get; set; }
        public int SearchQueryCount { get; set; }
        public int VenuesTotalCount { get; set; }
        public List<VenueModel> Venues { get; set; }
    }
}