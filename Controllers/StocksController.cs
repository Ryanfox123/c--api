using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StocksController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Stock>> GetAll()
        {
            var stocks = _context.Stocks.ToList();
            return stocks;
        }

        [HttpGet("{id}")]
        public ActionResult<Stock> GetById(int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null) return NotFound();
            
            return stock;
        }
    }
}