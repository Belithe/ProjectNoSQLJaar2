using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppClient.ViewModels
{
    public class TicketVM
    {
        public string Id { get; set; }

        [DisplayName("Date: ")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DisplayName("Ticket Subject: ")]
        public string Subject { get; set; }

        [DisplayName("Incident Type:  ")]
        public TypeOfIncidentEnum Type { get; set; }

        [DisplayName("Reported By User: ")]
        public User User { get; set; }

        [DisplayName("Priority: ")]
        public PriorityOfIncidentEnum Priority { get; set; }

        [DisplayName("Days Before Deadline: ")]
        public DeadlineEnum Deadline { get; set; }

        [DisplayName("Description: ")]
        public string Description { get; set; }

    }
}
