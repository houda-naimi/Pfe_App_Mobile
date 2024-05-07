using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PfeShell.Models
{
    [Table("Event")]
    public class AppointmentModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Color { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}
