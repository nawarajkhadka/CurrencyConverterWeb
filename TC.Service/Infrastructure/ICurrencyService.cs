using System;
using System.Collections.Generic;
using System.Text;
using TC.Model;

namespace TC.Service.Infrastructure
{
    public interface ICurrencyService
    {

                

        List<CurrencyResultSet> GetCurr(CurrencyRateInput input);

        //helper functions
        List<DropDownItems> GetDdls();


    }
}
