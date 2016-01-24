using System.Collections.Generic;

namespace FourSquareSolution.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Venues = new List<VenueModel>();
        }

        /// <summary>
        ///     Search query of venues
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        ///     How many times was query used before
        /// </summary>
        public int SearchQueryCount { get; set; }

        /// <summary>
        ///     Total venues count for used query
        /// </summary>
        public int VenuesTotalCount { get; set; }

        /// <summary>
        ///     List of VenueModels
        /// </summary>
        public List<VenueModel> Venues { get; set; }
    }
}