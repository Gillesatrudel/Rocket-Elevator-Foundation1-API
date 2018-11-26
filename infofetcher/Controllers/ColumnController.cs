using System.Collections.Generic;
using System.Linq;
using infofetcher.Models;
using Microsoft.AspNetCore.Mvc;

namespace infofetcher.Controllers
{
    [Route ("api/columns")]
    [ApiController]
    public class Columncontroller : ControllerBase 
    {
        private readonly gillesatContext _context;

        public Columncontroller (gillesatContext context) {
            _context = context;
            
        }

        [HttpGet]
        public ActionResult<List<Columns>> GetAll () {
            return _context.Columns.ToList ();
        }

        [HttpGet ("{id}", Name = "GetColumns")]
        public ActionResult<Columns> GetById (long id) {
            var item = _context.Columns.Find (id);
            if (item == null) {
                return NotFound ();
            }
            return item;
        }
    }
}