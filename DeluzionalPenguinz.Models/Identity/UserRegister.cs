using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.Models.Identity
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Το email πρέπει να υπάρχει καταρχάς."), EmailAddress (ErrorMessage ="Το email πρέπει να είναι σωστό email.")]
        public string Email { get; set; }
        [StringLength(16, ErrorMessage = "Το Όνομα Χρήστη δεν μπορεί να είναι πάνω απο 16 χαρακτήρες."),Required(ErrorMessage = "Το Όνομα Χρήστη πρέπει να υπάρχει.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Πως θα συνδεθείς χωρίς password? Πές μου.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Πως θα συνδεθείς χωρίς password? Πές μου.")]
        [Compare("Password", ErrorMessage = "Αμάν, τα Password δεν είναι ίδια.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public HumanType HumanType { get; set; }

        public string AM { get; set; } = "";

        [Required(ErrorMessage = "Το όνομα πρέπει να υπάρχει")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Το επώνυμο πρέπει να υπάρχει")]
        public string LastName { get; set; }

    }
}
