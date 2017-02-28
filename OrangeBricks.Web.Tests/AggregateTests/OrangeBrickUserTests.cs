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
    public class Given_An_Available_Property
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
    }

    [TestFixture]
    public class Given_OrangeBrick_User_With_Offers
    {
        IQueryable<Offer> offers;
        Mock<DbSet<Offer>> offersMock;
        Mock<IOrangeBricksContext> mockContext;
        OrangeBricksUser User;
        OrangeBrickUserOffersCollection pendingOffersCollection;
        OrangeBrickUserOffersCollection acceptedOffersCollection;
        OrangeBrickUserOffersCollection rejectedOffersCollection;

        [SetUp]
        public void SetupMocks()
        {
            mockContext = new Mock<IOrangeBricksContext>();

            offers = new List<Offer>
                {
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
                    },
                    new Offer
                    {
                        Amount = 10,
                        CreatedAt = DateTime.Now,
                        Id = 0,
                        Status = OfferStatus.Rejected,
                        UpdatedAt = DateTime.Now
                    }
                }.AsQueryable();

            offersMock = new Mock<DbSet<Offer>>();
            User = new OrangeBricksUser(mockContext.Object);

            offersMock.As<IQueryable<Offer>>().Setup(m => m.Provider).Returns(offers.Provider);
            offersMock.As<IQueryable<Offer>>().Setup(m => m.Expression).Returns(offers.Expression);
            offersMock.As<IQueryable<Offer>>().Setup(m => m.ElementType).Returns(offers.ElementType);
            offersMock.As<IQueryable<Offer>>().Setup(m => m.GetEnumerator()).Returns(offers.GetEnumerator());

            mockContext.Setup(x => x.Offers).Returns(offersMock.Object);
        }

        [Test]
        public void When_Pending_Offers_Are_Requested_Then_Pending_Offers_Are_Returned()
        {
            OrangeBrickUserOffersCollection pendingOffersCollection = User.GetPendingOffers();
            Assert.That(pendingOffersCollection.Count == 1);
            Assert.That(pendingOffersCollection[0].Status == OfferStatus.Pending);
        }

        [Test]
        public void When_Accepted_Offers_Are_Requested_Then_Accepted_Offers_Are_Returned()
        {
            OrangeBrickUserOffersCollection acceptedOffersCollection = User.GetAcceptedOffers();
            Assert.That(acceptedOffersCollection.Count == 1);
            Assert.That(acceptedOffersCollection[0].Status == OfferStatus.Accepted);
        }

        [Test]
        public void When_Rejected_Offers_Are_Requested_Then_Rejected_Offers_Are_Returned()
        {
            OrangeBrickUserOffersCollection rejectedOffersCollection = User.GetRejectedOffers();
            Assert.That(rejectedOffersCollection.Count == 1);
            Assert.That(rejectedOffersCollection[0].Status == OfferStatus.Rejected);
        }
    }

    //Setup a haschanged flag/Kick off event to update Property
}