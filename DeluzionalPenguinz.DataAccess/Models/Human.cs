using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.DataAccess.Models
{
    public class Human : IdentityUser 
    {
        public Human(string id) : base(id)
        {

        }
        public Human(string? firstName, string? lastName, string? aM,string?userName, HumanType humanType, ICollection<Anouncement>? anouncements)
        {
            FirstName = firstName;
            LastName = lastName;
            AM = aM;
            HumanType = humanType;
            Anouncements = anouncements;
            this.UserName = userName;
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AM { get; set; }
        public HumanType HumanType { get; set; }
        public ICollection<Anouncement>? Anouncements { get; set; }
    }
}
