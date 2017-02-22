using Moq;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OrangeBricks.Web.Tests.ModelTests
{
    [TestFixture]
    public class OrangeBrickUserTests
    {
        [Test]
        public void When_OrangeBrickUser_Makes_Offer_Then_Offer_Added_To_Database()
        {
            Mock<IOrangeBricksContext> mockContext = new Mock<IOrangeBricksContext>();
            mockContext.SetupProperty(x => x.Offers, new Mock<DbSet<Offer>>().Object);
            OrangeBricksUser User = new OrangeBricksUser(mockContext.Object);
            Offer offer = new Offer();
            User.MakeOffer(offer);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void When_OrangeBrickUser_Requests_All_Pending_Offers_List_Then_Pending_Offers_List_Is_Returned()
        {
            Mock<IOrangeBricksContext> mockContext = new Mock<IOrangeBricksContext>();

            //This is pretty horrible, not massively experienced with Mocking out Entity Framework classes.
            var offers = new List<Offer> {
                new Offer
                {
                    Amount = 10,
                    CreatedAt = DateTime.Now,
                    Id = 0,
                    Status = OfferStatus.Pending,
                    UpdatedAt = DateTime.Now
                },
                new Offer
                {
                    Amount = 10,
                    CreatedAt = DateTime.Now,
                    Id = 0,
                    Status = OfferStatus.Accepted,
                    UpdatedAt = DateTime.Now
                }
            }.AsQueryable();
            var offersMock = new Mock<DbSet<Offer>>();
            offersMock.As<IQueryable<Offer>>().Setup(m => m.Provider).Returns(offers.Provider);
            offersMock.As<IQueryable<Offer>>().Setup(m => m.Expression).Returns(offers.Expression);
            offersMock.As<IQueryable<Offer>>().Setup(m => m.ElementType).Returns(offers.ElementType);
            offersMock.As<IQueryable<Offer>>().Setup(m => m.GetEnumerator()).Returns(offers.GetEnumerator());
            //End Horribleness

            mockContext.Setup(x => x.Offers).Returns(offersMock.Object);
            OrangeBricksUser User = new OrangeBricksUser(mockContext.Object);
            OrangeBrickUserOffersCollection offersCollection = User.GetPendingOffers();
            Assert.That(offersCollection.Count == 1);
            Assert.That(offersCollection[0].Status == OfferStatus.Pending);
        }
    }
}
