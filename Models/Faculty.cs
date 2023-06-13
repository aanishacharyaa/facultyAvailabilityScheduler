using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace facultyAvailabilityScheduler.Models
{
    internal class Faculty
    {
 
    public int Id { get; set; } // The unique ID of the faculty member.
        public string Name { get; set; } // The name of the faculty member.
        public string Department { get; set; } // The department to which the faculty member belongs.
        public string Email { get; set; } // The email address of the faculty member.
        public string OfficeLocation { get; set; } // The office location of the faculty member.
        public List<Appointment> Availability { get; set; } // A list of time slots indicating the availability of the faculty member.
        public string CommunicationPlatform { get; set; } // The preferred communication platform for the faculty member.
 
}
}
