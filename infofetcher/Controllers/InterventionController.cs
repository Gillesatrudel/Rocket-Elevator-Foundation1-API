using System.Collections.Generic;
using System.Linq;
using infofetcher.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
namespace infofetcher.Controllers
{
    [Route ("api/interventions")]
    [ApiController]
    public class InterventionController : ControllerBase 
    {
        private readonly gillesatContext _context;

        public InterventionController (gillesatContext context) {
            _context = context;
            
        }

        [HttpGet]
        public ActionResult<List<Interventions>> GetAll () {
                return _context.Interventions.Where(x => x.BeginDate == null && x.Status == "pending").ToList();
        
        }


        [HttpPut("{id}", Name = "PutIntervationStatus")]
       public string Update(long id, [FromBody] JObject body)
       {

           var intervention = _context.Interventions.Find(id);
           if (intervention == null)
           {
               return "Enter a valid intervention id.";
           }

           var previous_status = intervention.Status;
           var status = (string)body.SelectToken("status");
           if (status == "in progress")
           {
               DateTime time = DateTime.Now;
               intervention.Status = status;
               intervention.BeginDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"));
               intervention.UpdatedAt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"));
               _context.Interventions.Update(intervention);
               _context.SaveChanges();
               return "The intervention #" + intervention.Id + " has changed status from " + previous_status + ", to " + status + ".";
            }   
            
            if (status == "complete")
            {
               DateTime time = DateTime.Now;
               intervention.Status = status;
               intervention.FinishDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"));
               intervention.UpdatedAt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"));
               _context.Interventions.Update(intervention);
               _context.SaveChanges();
               return "The intervention #" + intervention.Id + " has changed status from " + previous_status + ", to " + status + ".";
            }
           else
           {
               return "Invalid status: Must be In progress or Complete";
           }


     
        }
    }
} 
