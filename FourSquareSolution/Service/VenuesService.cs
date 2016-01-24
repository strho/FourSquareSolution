using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using FourSquare.SharpSquare.Core;
using FourSquareSolution.Constants;
using FourSquareSolution.Models;

namespace FourSquareSolution.Service
{
    public class VenuesService
    {
        private readonly SharpSquare _sharpSquare;

        public VenuesService()
        {
            var clientId = WebConfigurationManager.AppSettings["FourSquareClientID"];
            var clientSecret = WebConfigurationManager.AppSettings["FourSquareClientSecret"];
            this._sharpSquare = new SharpSquare(clientId, clientSecret);
        }

        public SearchModel GetVenuesModelForSearchQuery(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new SearchModel();
            }

            var parameters = this.GetParametersForSearchQuery(searchQuery);

            var searchModel = new SearchModel
            {
                SearchQuery = searchQuery,
                SearchQueryCount = 0,
                VenuesTotalCount = this.GetVenuesCount(parameters),
                Venues = this._sharpSquare
                    .SearchVenues(parameters)
                    .Take(10)
                    .Select(venue => new VenueModel
                    {
                        Id = venue.id,
                        Name = venue.name,
                        CheckinCount = venue.stats.checkinsCount,
                        NowHere = venue.hereNow.count
                    }).ToList()
            };


            return searchModel;
        }

        public IEnumerable<VenueModel> GetMoreVenuesForSearchQuery(string searchQuery, int skipCount)
        {
            var parameters = this.GetParametersForSearchQuery(searchQuery);

            var moreVenues = this._sharpSquare.SearchVenues(parameters)
                .Skip(skipCount)
                .Take(10)
                .Select(venue => new VenueModel
                {
                    Id = venue.id,
                    Name = venue.name,
                    CheckinCount = venue.stats.checkinsCount,
                    NowHere = venue.hereNow.count
                }).ToList();

            return moreVenues;
        }

        #region private

        private int GetVenuesCount(Dictionary<string, string> parameters)
        {
            return this._sharpSquare.SearchVenues(parameters).Count;
        }


        private Dictionary<string, string> GetParametersForSearchQuery(string searchQuery,
            string location = FourSquareConstants.BrnoLocation)
        {
            return new Dictionary<string, string>
            {
                {"near", location},
                {"query", searchQuery}
            };
        }

        #endregion
    }
}