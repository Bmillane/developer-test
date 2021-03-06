using System;
using System.Collections.Generic;
using OrangeBricks.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public MakeOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(MakeOfferCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var offer = new Offer
            {
                Amount = command.Offer,
                Status = Status.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Property = property,
                UserId = command.UserId
            };

            if (property.Offers == null)
            {
                property.Offers = new List<Offer>();
            }
                
            property.Offers.Add(offer);
            
            _context.SaveChanges();
        }
    }
}