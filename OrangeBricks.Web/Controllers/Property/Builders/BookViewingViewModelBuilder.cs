using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class BookViewingViewModelBuilder
    {
        IOrangeBricksContext context;

        public BookViewingViewModelBuilder(IOrangeBricksContext context)
        {
            this.context = context;
        }

        public BookViewingViewModel Build(int propertyId)
        {
            var property = context.Properties.Find(propertyId);

            //Could expand this to obtain User/Seller for this property
            //Would then change the seller to have a collection of viewing dates
            //Would then pre-populate the viewing time with the earliest possible date.
            //This would then stop the test potentially failing if it was ran just as the date was changing.

            return new BookViewingViewModel
            {
                Property = property,
                ViewingTime = new DateTime(DateTime.Now.Ticks).AddDays(1)
            };
        }
    }
}