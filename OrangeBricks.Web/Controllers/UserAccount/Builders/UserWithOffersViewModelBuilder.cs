using OrangeBricks.Web.Controllers.UserAccount.ViewModels;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.UserAccount.Builders
{
    public class UserWithOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;
        private readonly ApplicationUser _user;

        public UserWithOffersViewModelBuilder(IOrangeBricksContext context, ApplicationUser user)
        {
            _context = context;
            _user = user;
        }

        public UserWithOffersViewModel Build()
        {
            var offers = _user.Offers ?? new List<Offer>();

            return new UserWithOffersViewModel
            {
                Offers = offers.Select(x => new OfferViewModel
                {
                        Id = x.Id,
                        Amount = x.Amount,
                        CreatedAt = x.CreatedAt,
                        IsPending = x.Status == OfferStatus.Pending,
                        Status = x.Status.ToString()
                }),
                UserId = _user.Id,
                UserName = _user.UserName,
                HasOffers = offers.Any(),
                OffersAccepted = offers.Where(x => x.Status == OfferStatus.Accepted).Count(),
                OffersPending = offers.Where(x => x.Status == OfferStatus.Pending).Count(),
                OffersRejected = offers.Where(x => x.Status == OfferStatus.Rejected).Count()
            };
        }
    }
}