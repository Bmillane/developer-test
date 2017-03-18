﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommand
    {
        public string UserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime ViewingTime { get; set; }
    }
}