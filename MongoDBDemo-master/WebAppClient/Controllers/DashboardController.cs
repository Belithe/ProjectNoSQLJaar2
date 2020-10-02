using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver.Core.Operations;
using WebAppClient.ViewModels;

namespace WebAppClient.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(DashboardVM model)
        {

            TicketController controller = new TicketController();

            var tickets = await controller.fillTicket();
                
            var ticketsAll = from e in tickets
                             select e;
            model.ticketsTotal = new List<Ticket>(ticketsAll);

            var ticketsOpen = from e in tickets //TO-Do, add property with open/closed status
                             select e;
            model.ticketsOpen = new List<Ticket>(ticketsOpen);


            //Ticket overdue
            foreach (var item in ticketsOpen) 
            {
                switch (item.Deadline)
                {
                    case DeadlineEnum.one:
                        var dueDate1 = item.DateTime.AddDays(1);
                        if (dueDate1 < DateTime.Now)
                        {
                            model.ticketsOverdue.Add(item);
                        }
                        break;

                    case DeadlineEnum.seven:
                        var dueDate2 = item.DateTime.AddDays(7);
                        if (dueDate2 < DateTime.Now)
                        {
                            model.ticketsOverdue.Add(item);
                        }
                        break;

                    case DeadlineEnum.thirty:
                        var dueDate3 = item.DateTime.AddDays(30);
                        if (dueDate3 < DateTime.Now)
                        {
                            model.ticketsOverdue.Add(item);
                        }
                        break;
                }
            }

            return View(model);
        }
    }
}
