using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace PfeShell.Models
{
    public class AppelTel
    {
        public string CallName { get; set; }
        public string CallNumber { get; set; }
        public long CallDateTick { get; set; }
        public DateTime CallDate
        {
            get
            {
                return new DateTime(1970, 1, 1, 1, 0, 0, DateTimeKind.Local).AddMilliseconds(this.CallDateTick);
            }
        }

        public string CallType { get; set; }

        public string CallTitle { get => $"{CallNumber} - {CallName}"; }
        public string Date {
            get
            {
                return this.CallDate.Date.ToString("dd/MMM/yyyy");
            }
        }
        public string Time
        {
            get
            {
                return this.CallDate.ToString("HH:mm");
            }
        }
        public AppelTel(){
        }
    }
}
