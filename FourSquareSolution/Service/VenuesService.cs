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
        private readonly SearchQueryService _queryService;
        private readonly SharpSquare _sharpSquare;

        public VenuesService()
        {
            var clientId = WebConfigurationManager.AppSettings["FourSquareClientID"];
            var clientSecret = WebConfigurationManager.AppSettings["FourSquareClientSecret"];
            this._queryService = new SearchQueryService();
            this._sharpSquare = new SharpSquare(clientId, clientSecret);
        }

        /// <summary>
        ///     Reads venues from FourSquare API and store searchQuery in DB
        /// </summary>
        /// <param name="searchQuery">searchQuery used by user</param>
        /// <returns>model with first 10 venues from FourSquare API</returns>
        public SearchModel GetVenuesModelForSearchQuery(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new SearchModel();
            }

            var parameters = this.GetParametersForSearchQuery(searchQuery);
            var query = this._queryService.ExistsSearchQuery(searchQuery)
                ? this._queryService.UpdateSearchQuery(searchQuery)
                : this._queryService.CreateAndSaveSearchQuery(searchQuery);

            var searchModel = new SearchModel
            {
                SearchQuery = searchQuery,
                SearchQueryCount = query.Count,
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

        /// <summary>
        ///     Read more venues from FourSquare API
        /// </summary>
        /// <param name="searchQuery">searchQuery used by user</param>
        /// <param name="skipCount">how many venues are already displayed to user</param>
        /// <returns>list of venues which were not yet displayed to user</returns>
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

        /// <summary>
        ///     Gets total number of venues returned from FourSquare API for provided parameters
        /// </summary>
        /// <param name="parameters">search parameters for FourSquare API</param>
        /// <returns>count of venues</returns>
        private int GetVenuesCount(Dictionary<string, string> parameters)
        {
            return this._sharpSquare.SearchVenues(parameters).Count;
        }

        /// <summary>
        ///     Creates parameters for provides searchString
        /// </summary>
        /// <param name="searchQuery">searchedString used by user</param>
        /// <param name="location">location where are the venues looked for. Defautlt location is Brno</param>
        /// <returns>parameters dictionary</returns>
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