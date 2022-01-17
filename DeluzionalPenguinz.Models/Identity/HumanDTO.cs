using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.Models.Identity
{

    public class HumanDTO
    {
        public HumanDTO()
        {

        }
        public HumanDTO(string id, string username, string? firstName, string? lastName, string? aM, HumanType humanType, ICollection<AnouncementDTO>? anouncements)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            AM = aM;
            HumanType = humanType;
            Anouncements = anouncements;


            FullName = FirstName + " " + LastName;

        }
        public string FullName { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AM { get; set; }
        public HumanType HumanType { get; set; }
        public ICollection<AnouncementDTO>? Anouncements { get; set; }
    }

}
