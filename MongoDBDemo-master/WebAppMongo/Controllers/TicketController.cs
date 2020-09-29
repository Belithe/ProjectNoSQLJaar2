using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.TicketServices;
using Services.UserServices;

namespace WebAppMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;
        private readonly IUserService _uService;

        public TicketController(ITicketService service, IUserService uService)
        {
            this._service = service;
            _uService = uService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTicket()
        {
            return Ok(_service.GetAllTickets() as List<Ticket>);
        }

        [Route("/api/ticket/countticket/")]
        public ActionResult<long> CountTicket()
        {
            return Ok(_service.CountThemAll());
        }

        [Route("/api/ticket/GetTicketById/{id}")]
        public ActionResult<Ticket> GetTicketById(string id)
        {
            Ticket ticket = _service.GetSingle(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        [Route("/api/Ticket/CreateTicket/", Name = "Create Ticket")]
        public ActionResult<Ticket> CreateTicket(Ticket ticket)
        {

            if (ticket == null)
            {
                return BadRequest();
            }
            _service.AddTicket(ticket);

            return Ok(ticket);
        }

        [HttpPatch]
        [Route("/api/ticket/updateticket/{id}", Name = "Update Ticket")]
        public IActionResult UpdateTicket(string id, [FromBody] JsonPatchDocument<Ticket> patchDoc)
        {
            Ticket foundTicket = _service.GetSingle(id);
            patchDoc.ApplyTo(foundTicket);
            _service.UpdateTicket(foundTicket);

            return Ok();
        }



        [Route("/api/ticket/deleteticket/{id}")]
        public IActionResult DeleteTicket(string id)
        {
            Ticket ticket = _service.GetSingle(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _service.RemoveTicket(ticket);

            return Ok();
        }
    }
}
