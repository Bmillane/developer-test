using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Property.Commands;
using NSubstitute;
using System.Data.Entity;

namespace OrangeBricks.Web.Tests.Controllers.Property
{
    [TestFixture]   
    public class Given_A_User
    {
        IOrangeBricksContext context;

        [TestFixtureSetUp]
        public void When_User_Books_Viewing()
        {
            context = Substitute.For<IOrangeBricksContext>();
            context.Properties.Find(1).Returns(new Models.Property { Id = 1 });
            context.Viewings.Returns(Substitute.For<IDbSet<Models.Viewing>>());
            BookViewingCommandHandler handler = new BookViewingCommandHandler(this.context);
            BookViewingCommand command = new BookViewingCommand
            {
                PropertyId = 1,
                UserId = "test",
                ViewingTime = new DateTime(2017, 01, 03, 10, 10, 0)
            };
            handler.Handle(command);
        }

        [Test]
        public void Then_Viewing_Is_Saved_With_Correct_Date()
        {
            context.Viewings.Received(1).Add(Arg.Is<Viewing>(x => x.ViewingTime == new DateTime(2017, 01, 03, 10, 10, 0)));
        }

        [Test]
        public void Then_Viewing_Is_Saved_With_Correct_User_Id()
        {
            context.Viewings.Received(1).Add(Arg.Is<Viewing>(x => x.UserId == "test"));
        }

        [Test]
        public void Then_Viewing_Is_Saved_With_Correct_PropertyId()
        {
            context.Viewings.Received(1).Add(Arg.Is<Viewing>(x => x.Property.Id == 1));
        }

        [Test]
        public void Then_Viewing_Is_Saved_With_Pending_Status()
        {
            context.Viewings.Received(1).Add(Arg.Is<Viewing>(x => x.Status == Status.Pending));
        }


    }
}
