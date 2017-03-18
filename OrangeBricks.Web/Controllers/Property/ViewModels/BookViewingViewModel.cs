using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class BookViewingViewModel
    {
        public Models.Property Property { get; set; }
        public DateTime ViewingTime { get; set; }
    }
}