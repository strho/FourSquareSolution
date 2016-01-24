using System.ComponentModel;
using FourSquareSolution.Constants;

namespace FourSquareSolution.Models
{
    public class VenueModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Checkin count")]
        public long CheckinCount { get; set; }

        [DisplayName("Now here")]
        public long NowHere { get; set; }

        public string Url => FourSquareConstants.FourSquareUrl + Name + "/" + Id;
    }
}