using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TC.Context;
using TC.Model;
using TC.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TC.Service.CurrencyService
{
    public class CurrencyService : ICurrencyService
    {
        public CurrencyContext _context;
        public CurrencyService(CurrencyContext context)
        {
            _context = context;
        }

        //get currency list service
        public List<CurrencyResultSet> GetCurr(CurrencyRateInput input)
        {
            SqlDataReader rdr = null;
            List<CurrencyResultSet> currencyresults = new List<CurrencyResultSet>();
            using (SqlConnection conn = new SqlConnection("Server=LENOVOPC;Database=CurrencyConverter;Trusted_Connection=True;MultipleActiveResultSets=True;"))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand("[SP_GETRATESBYDATE]", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Amount", input.amount));
                cmd.Parameters.Add(new SqlParameter("@FromCurrency", input.sourcecurrency));
                cmd.Parameters.Add(new SqlParameter("@ToCurrency", input.destinationcurrency));
                cmd.Parameters.Add(new SqlParameter("@FromDate", input.date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", input.todate));
                cmd.Parameters.Add(new SqlParameter("@IsHistoric", input.ishistoric));

                using (rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {


                        CurrencyResultSet result = new CurrencyResultSet()
                        {
                            Date = Convert.ToDateTime(rdr["Date"].ToString()),
                            SourceCurrency = rdr["SourceCurrency"].ToString(),
                            DestinationCurrency = rdr["DestinationCurrency"].ToString(),
                            ConvertedAmount = rdr["ConvertedAmount"].ToString()

                        };
                        currencyresults.Add(result);
                    }
                }
            }
            return currencyresults;
        }


        #region "helper functions"
        //get drop down list
        public List<DropDownItems> GetDdls()
        {
            List<DropDownItems> ddls = new List<DropDownItems>();
            var namelist = _context.CurrencyRates.ToList().Select(o => o.currency).Distinct();

            foreach (var cname in namelist)
            {
                DropDownItems ddItem = new DropDownItems()
                {
                    Name = cname,
                    Value = cname
                };
                ddls.Add(ddItem);
            }


            return ddls;
        }

        
        #endregion
    }



}
