using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using be.Extensions;
using be.Interfaces;
using be.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace be.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> usermanager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = usermanager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appuser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePortfolio([FromBody] string symbol)
        {
            var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                return BadRequest($"stock with symbol: {symbol} not found");
            }
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appuser);
            if (userPortfolio.Any(e => e.Symbol == stock.Symbol))
            {
                return BadRequest("cant create port because trung symbol");
            }
            var portfolioModel = new Portfolio
            {
                StockId = stock.ID,
                AppUserId = appuser.Id
            };
            await _portfolioRepo.CreateAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return StatusCode(500, "cant create");
            }
            else
            {
                return Created();
            }

        }
    }
}