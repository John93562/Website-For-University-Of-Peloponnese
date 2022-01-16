using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.Models.Identity
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Το Όνομα Χρήστη Είναι Σημαντικό.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Πώς θα συνδεθείς χωρίς Password??")]
        public string Password { get; set; }
    }
}
