using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClient.Helpers;
using WebAppClient.ViewModels;
using Models;
using Marvin.JsonPatch;
using WebAppClient.Models;
using System.Diagnostics;

namespace WebAppClient.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = MVCClientHttpClient.GetClient();
            HttpResponseMessage userResponse = await client.GetAsync("api/Ticket/");

            TicketsVM ticketsVM = new TicketsVM();

            if (userResponse.IsSuccessStatusCode)
            {
                string Content = await userResponse.Content.ReadAsStringAsync();
                ticketsVM.lstTickets = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(Content);
            }
            else
            {
                return Content("An error occurred.");
            }

            return View(ticketsVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TicketsVM model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.TextSearch))
            {
                HttpClient client = MVCClientHttpClient.GetClient();
                HttpResponseMessage userResponse = await client.GetAsync("api/ticket/");

                if (userResponse.IsSuccessStatusCode)
                {
                    string Content = await userResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(Content);
                    model.lstTickets = result.Where(x => x.Subject.Contains(model.TextSearch));
                }
                else
                {
                    return Content("An error occurred.");
                }
                return View(model);
            }
            else
            {
                return View(new TicketsVM());
            }
        }

        public async Task<IActionResult> CountThem()
        {
            long noOfTickets;
            HttpClient client = MVCClientHttpClient.GetClient();
            HttpResponseMessage userResponse = await client.GetAsync("/api/ticket/countticket/");

            if (userResponse.IsSuccessStatusCode)
            {
                string Content = await userResponse.Content.ReadAsStringAsync();
                noOfTickets = JsonConvert.DeserializeObject<long>(Content);

            }
            else
            {
                return Content("An error occurred.");
            }
            return Content(noOfTickets.ToString());
        }

        public async Task<IActionResult> Get(string id)
        {
            HttpClient client = MVCClientHttpClient.GetClient();

            HttpResponseMessage userResponse = await client.GetAsync("/api/ticket/getticketbyid/" + id);

            if (userResponse.IsSuccessStatusCode)
            {
                string Content = await userResponse.Content.ReadAsStringAsync();
                var foundTicket = JsonConvert.DeserializeObject<User>(Content);
            }
            else
            {
                return Content("Ticket not found.");
            }
            return null;
        }


        [HttpGet]
        public IActionResult Create()
        {
            TicketVM ticketVM = new TicketVM();
            return View(ticketVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketVM ticketVM)
        {
            HttpClient client = MVCClientHttpClient.GetClient();
            var SerializedItemToCreate = JsonConvert.SerializeObject(ticketVM);

            HttpResponseMessage userResponse = await client.PostAsync("/api/ticket/createticket/",
                                                new StringContent(SerializedItemToCreate,
                                                System.Text.Encoding.Unicode,
                                                "application/json"));

            if (userResponse.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index");
            }
            return this.RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            HttpClient client = MVCClientHttpClient.GetClient();
            HttpResponseMessage userResponse = await client.GetAsync("/api/ticket/getticketbyid/" + Id);

            if (userResponse.IsSuccessStatusCode)
            {

                TicketVM ticketVM = new TicketVM();

                string Content = await userResponse.Content.ReadAsStringAsync();
                Ticket foundTicket = JsonConvert.DeserializeObject<Ticket>(Content);
                ticketVM.Id = foundTicket.Id;
                ticketVM.DateTime = foundTicket.DateTime;
                ticketVM.Subject = foundTicket.Subject;
                ticketVM.Type = foundTicket.Type;
                ticketVM.User = foundTicket.User;
                ticketVM.Priority = foundTicket.Priority;
                ticketVM.Deadline = foundTicket.Deadline;
                ticketVM.Description = foundTicket.Description;

                return View(ticketVM);
            }
            return Content("An error occurred.");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketVM ticketVM)
        {
            JsonPatchDocument<Ticket> patchDoc = new JsonPatchDocument<Ticket>();
            patchDoc.Replace(e => e.DateTime, ticketVM.DateTime);
            patchDoc.Replace(e => e.Subject, ticketVM.Subject);
            patchDoc.Replace(e => e.Type, ticketVM.Type);
            patchDoc.Replace(e => e.User, ticketVM.User);
            patchDoc.Replace(e => e.Priority, ticketVM.Priority);
            patchDoc.Replace(e => e.Deadline, ticketVM.Deadline);
            patchDoc.Replace(e => e.Description, ticketVM.Description);

            //serialize patch
            var serializedPatch = JsonConvert.SerializeObject(patchDoc);

            HttpClient client = MVCClientHttpClient.GetClient();

            HttpResponseMessage userResponse = await client.PatchAsync("/api/ticket/updateticket/" + id,
                                                new StringContent(serializedPatch, System.Text.Encoding.Unicode,
                                                "application/json"));


            if (userResponse.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index");
            }

            return Content("An error occurred.");
        }

        public async Task<IActionResult> Delete(string Id)
        {
            HttpClient client = MVCClientHttpClient.GetClient();
            HttpResponseMessage userResponse = await client.DeleteAsync("/api/ticket/deleteticket/" + Id);

            if (userResponse.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index");
            }
            return Content("An error occurred.");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }



}
