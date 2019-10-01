using System;
using System.Collections.Generic;
using System.Linq;
using GqlWorkshop.Controllers.Models;
using GqlWorkshop.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly AppDbContext db;

        public QuotesController(AppDbContext db)
        {
            this.db = db;
        }

        // GET api/quotes
        [HttpGet]
        public ActionResult<IEnumerable<Quote>> Get()
        {
            return db.Quotes.ToList();
        }

        // GET api/quotes/5
        [HttpGet("{id}")]
        public ActionResult<Quote> Get(long id, bool includeComments = false)
        {
            var query = db.Quotes.Where(x => x.Id == id);

            if (includeComments)
                query = query.Include(x => x.Comments);

            return query.FirstOrDefault();
        }

        // POST api/quotes
        [HttpPost]
        public ActionResult<Quote> Post([FromBody] QuoteModel model)
        {
            var quote = new Quote {
                CreatedAt = DateTime.UtcNow,
                Text = model.Text,
                SaidBy = model.SaidBy
            };

            db.Quotes.Add(quote);
            db.SaveChanges();

            return CreatedAtAction("Get", new {id = quote.Id}, quote);
        }

        // PUT api/quotes/5
        [HttpPut("{id}")]
        public ActionResult<Quote> Put(long id, [FromBody] QuoteModel model)
        {
            var quote = db.Quotes.Find(id);
            if (quote == null) return NotFound();

            quote.Text = model.Text;
            quote.SaidBy = model.SaidBy;

            db.SaveChanges();

            return Ok(quote);
        }

        // DELETE api/quotes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var quote = db.Quotes.Find(id);
            if (quote == null) return NotFound();

            db.Quotes.Remove(quote);
            db.SaveChanges();

            return NoContent();
        }
    }
}
