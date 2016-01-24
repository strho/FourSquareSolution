using System.ComponentModel;
using FourSquareSolution.Constants;

namespace FourSquareSolution.Models
{
    public class VenueModel
    {
        /// <summary>
        ///     Id of venue used for creating URL to FourSquare websites
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Name of venue
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     How many people has visited venue
        /// </summary>
        [DisplayName("Checkin count")]
        public long CheckinCount { get; set; }

        /// <summary>
        ///     How many people are currently at venue
        /// </summary>
        [DisplayName("Now here")]
        public long NowHere { get; set; }

        /// <summary>
        ///     URL of venue at FourSquare websites
        /// </summary>
        public string Url => FourSquareConstants.FourSquareUrl + Name + "/" + Id;
    }
}