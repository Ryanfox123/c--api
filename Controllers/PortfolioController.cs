using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly IStockRepository _stockRepo;
        public PortfolioController(UserManager<AppUser> userManager,
        IStockRepository stockRepo)
        {
            _UserManager = userManager;
            _stockRepo = stockRepo;
        }
    }
}