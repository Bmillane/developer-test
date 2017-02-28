using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace OrangeBricks.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //Aggregate Root
    public class OrangeBricksUser : ApplicationUser
    {
        private readonly IOrangeBricksContext context;

        public OrangeBricksUser(IOrangeBricksContext context)
        {
            if(context == null)
            {
                throw new NullReferenceException();
            }

            this.context = context;
        }

        public void MakeOffer(Offer offer)
        {
            context.Offers.Add(offer);
            context.SaveChanges();
        }

        public OrangeBrickUserOffersCollection GetPendingOffers()
        {
            OrangeBrickUserOffersCollection offers = new OrangeBrickUserOffersCollection();
            AddSpecificOfferType(OfferStatus.Pending, offers);
            return offers;
        }

        public OrangeBrickUserOffersCollection GetAcceptedOffers()
        {
            OrangeBrickUserOffersCollection offers = new OrangeBrickUserOffersCollection();
            AddSpecificOfferType(OfferStatus.Accepted, offers);
            return offers;
        }

        public OrangeBrickUserOffersCollection GetRejectedOffers()
        {
            OrangeBrickUserOffersCollection offers = new OrangeBrickUserOffersCollection();
            AddSpecificOfferType(OfferStatus.Rejected, offers);
            return offers;
        }

        private void AddSpecificOfferType(OfferStatus status, OrangeBrickUserOffersCollection offers)
        {
            foreach (var x in context.Offers)
            {
                if (x.Status == status)
                {
                    offers.Add(x);
                }
            }
        }

    }

    //Aggregate
    public class OrangeBrickUserOffersCollection : Collection<Offer>
    {
    }
}