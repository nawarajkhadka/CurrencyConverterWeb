using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TC.Service.Infrastructure;

using TC.Model;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Dynamic;

namespace TCWEBAPI.Controllers
{
    [Route("api/Currencies")]

    [ApiController]
    public class CurrencyAPIController : ControllerBase
    {
        private readonly ICurrencyService _context;
        public CurrencyAPIController(ICurrencyService context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CurrencyRates>> Get()
        {

            return _context.GetCurrencies();

        }


        [Route("GetExchangeRate")]
        [HttpGet]
        public ActionResult<CurrencyRateOutput> GetExchangedRate  ([FromBody] CurrencyRateInput currencyinput  )
        {
            float exchangedAmount= _context.GetExchangeRate(currencyinput);
            CurrencyRateOutput cop = new CurrencyRateOutput();
            cop.date = currencyinput.date;
            cop.exchangedamount = exchangedAmount;
            if (cop.exchangedamount>0)
            {
                return Ok(cop);
            }
            else
            {
                return NotFound();
            }

        }



    }

}