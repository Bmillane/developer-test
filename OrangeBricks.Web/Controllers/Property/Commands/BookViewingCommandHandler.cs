using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommandHandler
    {
        IOrangeBricksContext context;

        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            this.context = context;
        }

        public void Handle(BookViewingCommand command)
        {
            var property = context.Properties.Find(command.PropertyId);

            var viewing = new Viewing
            {
                ViewingTime = command.ViewingTime,
                Property = property,
                UserId = command.UserId,
                Status = Status.Pending
            };

            if (property.Viewings == null)
            {
                property.Viewings = new List<Viewing>();
            }

            context.Viewings.Add(viewing);

            context.SaveChanges();
        }
    }
}
