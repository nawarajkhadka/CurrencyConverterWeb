using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TC.Model;
using TC.Service.CurrencyService;
using TC.Service.Infrastructure;

namespace TCWEB.Controllers.API
{
    [Route("api/Currencies")]
    [ApiController]
    public class CurrencyAPIController : ControllerBase
    {
        private readonly ICurrencyService _cService;


        public CurrencyAPIController(ICurrencyService cService)
        {
            _cService = cService;
        }


        //get currency list

        [HttpGet]
        [Route("GetCurrency")]

        public ActionResult<IEnumerable<CurrencyResultSet>> GetCurrencyData([FromQuery] string sourcecurrency,
            [FromQuery]string destinationcurrency, [FromQuery] DateTime? date, [FromQuery] DateTime? todate, [FromQuery] float amount, [FromQuery] bool ishistoric)
        {

            CurrencyRateInput input = new CurrencyRateInput()
            {
                sourcecurrency = sourcecurrency,
                destinationcurrency = destinationcurrency,
                date = date,
                amount = amount,
                todate = todate,
                ishistoric = ishistoric
            };
            var currencylist = _cService.GetCurr(input);


            if (currencylist == null||currencylist.Count()==0)
            {
                return NotFound("Exchange rate not found for date provided");
            }

            return currencylist;
        }

        //load drop down

        [HttpGet]
        [Route("loadCurrencyList")]
        public ActionResult<List<DropDownItems>> loadCurrencyList()
        {
            List<DropDownItems> items = new List<DropDownItems>();
            try
            {
                items = _cService.GetDdls();
                if (items.Count > 0)
                {
                    return Ok(items);
                }
                else
                {
                    return NotFound("Error loading currencies");
                }
            }
            catch(Exception ex)
            {
                return NotFound("Error loading currencies");
            }
            
            

        }



    }
}