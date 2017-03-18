using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime ViewingTime { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public virtual Property Property { get; set; }
    }
}