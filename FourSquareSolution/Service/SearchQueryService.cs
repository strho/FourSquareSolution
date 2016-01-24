using System.Linq;
using FourSquareSolution.DbContext;
using FourSquareSolution.Entity;

namespace FourSquareSolution.Service
{
    public class SearchQueryService
    {
        private readonly SearchQueryContext _db;

        public SearchQueryService()
        {
            this._db = new SearchQueryContext();
        }

        /// <summary>
        ///     Check if searchQuery already exists in DB
        /// </summary>
        /// <param name="searchQuery">searchQuery used by user</param>
        /// <returns>true if searchQuery exists otherwise false</returns>
        public bool ExistsSearchQuery(string searchQuery)
        {
            return this._db.SearchQueries.Any(query => query.Query == searchQuery);
        }

        /// <summary>
        ///     If query doesn't exist, create it and save it to DB
        /// </summary>
        /// <param name="searchQuery">searchQuery used by user</param>
        /// <returns>created query</returns>
        public SearchQuery CreateAndSaveSearchQuery(string searchQuery)
        {
            var query = new SearchQuery
            {
                Query = searchQuery,
                Count = 1
            };

            this._db.SearchQueries.Add(query);
            this._db.SaveChanges();

            return query;
        }

        /// <summary>
        ///     Increment count of searchQuery usage
        /// </summary>
        /// <param name="searchQuery">searchQuery used by user</param>
        /// <returns>updated query</returns>
        public SearchQuery UpdateSearchQuery(string searchQuery)
        {
            var query = this._db.SearchQueries.First(q => q.Query == searchQuery);
            query.Count += 1;

            this._db.SaveChanges();
            return query;
        }
    }
}