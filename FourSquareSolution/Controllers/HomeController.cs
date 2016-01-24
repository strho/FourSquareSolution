using System.Web.Mvc;
using FourSquareSolution.Models;
using FourSquareSolution.Service;

namespace FourSquareSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly VenuesService _service = new VenuesService();

        /// <summary>
        ///     Display view with empty model
        /// </summary>
        /// <returns>Index page with empty SearchModel</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new SearchModel());
        }

        /// <summary>
        ///     Display first 10 venues according to used "searchQuery"
        /// </summary>
        /// <param name="model">Show model containing "searchQuery"</param>
        /// <returns>Partial view of filled table with venues values</returns>
        [HttpPost]
        public ActionResult SearchVenues(SearchModel model)
        {
            if (string.IsNullOrEmpty(model.SearchQuery) || string.IsNullOrWhiteSpace(model.SearchQuery))
            {
                model.SearchQuery = null;
                model.Venues.Clear();
                model.VenuesTotalCount = 0;
                return this.PartialView("_VenuesPartialView", model);
            }

            model = this._service.GetVenuesModelForSearchQuery(model.SearchQuery);
            return this.PartialView("_VenuesPartialView", model);
        }

        /// <summary>
        ///     Display more requested venues
        /// </summary>
        /// <param name="searchQuery">query for wanted venues</param>
        /// <param name="currentVenuesCount">count of venues currently shown</param>
        /// <returns>Partial view of updated table with new venues values</returns>
        [HttpPost]
        public ActionResult ShowMoreVenues(string searchQuery, int currentVenuesCount)
        {
            var moreVenues = this._service.GetMoreVenuesForSearchQuery(searchQuery, currentVenuesCount);
            return this.PartialView("_ShowMorePartialView", moreVenues);
        }
    }
}