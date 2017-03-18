using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    [TestFixture]
    public class Given_A_Request_For_A_BookViewingViewModel
    {
        private IOrangeBricksContext _context;
        private BookViewingViewModel viewModel;

        [TestFixtureSetUp]
        public void When_ViewModel_Is_Returned()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            var builder = new BookViewingViewModelBuilder(_context);

            _context.Properties.Find(1).Returns(new Models.Property { Id = 1, SellerUserId = "test" });

            viewModel = builder.Build(1);
        }

        [Test]
        public void Then_It_Contains_The_Correct_Property()
        {
            Assert.That(viewModel.Property.SellerUserId == "test");
        }

        [Test]
        public void Then_It_Contains_A_Suggested_Viewing_Time()
        {
            //This is a bad test and could break if ran a split second before midnight.
            //Suggested improvement added in to the ViewModelBuilder.
            Assert.That(viewModel.ViewingTime.Day == new DateTime(DateTime.Now.Ticks).AddDays(1).Day);
        }

    }
}
