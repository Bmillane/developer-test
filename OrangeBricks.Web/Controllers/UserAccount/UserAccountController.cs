using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrangeBricks.Web.Controllers.UserAccount.Builders;
using OrangeBricks.Web.Models;
using System.Web;
using System.Web.Mvc;

namespace OrangeBricks.Web.Controllers.UserAccount
{
    public class UserAccountController : Controller
    {
        private readonly IOrangeBricksContext _context;
        private ApplicationUserManager _userManager;

        public UserAccountController(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ActionResult UserOffers(string id)
        {
            var user = UserManager.FindById(id);
            var builder = new UserWithOffersViewModelBuilder(_context, user);
            var viewModel = builder.Build();

            return View(viewModel);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}