using OrangeBricks.Web.Controllers.Offers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.UserAccount.ViewModels
{
    public class UserWithOffersViewModel
    {
        public IEnumerable<OfferViewModel> Offers { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool HasOffers { get; set; }
        public int OffersAccepted { get; set; }
        public int OffersRejected { get; set; }
        public int OffersPending { get; set; }
    }
}