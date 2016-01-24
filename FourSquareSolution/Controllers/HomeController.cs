using System.Web.Mvc;
using FourSquareSolution.Models;
using FourSquareSolution.Service;

namespace FourSquareSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly VenuesService _service = new VenuesService();

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new SearchModel());
        }

        [HttpPost]
        public ActionResult SearchVenues(SearchModel model)
        {
            if (string.IsNullOrEmpty(model.SearchQuery))
            {
                model.Venues.Clear();
                model.VenuesTotalCount = 0;
                return this.PartialView("_VenuesPartialView", model);
            }

            model = this._service.GetVenuesModelForSearchQuery(model.SearchQuery);
            return this.PartialView("_VenuesPartialView", model);
        }

        [HttpPost]
        public ActionResult ShowMoreVenues(string searchQuery, int currentVenuesCount)
        {
            var moreVenues = this._service.GetMoreVenuesForSearchQuery(searchQuery, currentVenuesCount);
            return this.PartialView("_ShowMorePartialView", moreVenues);
        }
    }
}