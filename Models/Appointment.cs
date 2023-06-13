using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace facultyAvailabilityScheduler.Models
{
    internal class Appointment
    {
        public int Id { get; set; } // The unique ID of the appointment.
        public Student Student { get; set; } // The name of the student who scheduled the appointment.
        public Faculty Faculty { get; set; } // The name of the faculty member who will be holding the appointment.
        public string Location { get; set; } // The location of the appointment, if applicable.
        public DateTime StartTime { get; set; } // The start time of the appointment.
        public DateTime EndTime { get; set; } // The end time of the appointment.
        public string CommunicationPlatform { get; set; } // The preferred communication platform for the appointment.
    }
}
