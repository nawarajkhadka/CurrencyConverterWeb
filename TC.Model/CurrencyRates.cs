using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TC.Model
{
    //base class to map with database
    public class CurrencyRates
    {
        public int ID { get; set; }

        public string @base { get; set; }
        public DateTime? date { get; set; }
        public string currency { get; set; }
        public double? rate { get; set; }


    }

    //custom class to bind currency input
    public class CurrencyRateInput
    {

        public string sourcecurrency { get; set; }

        public string destinationcurrency { get; set; }

        public float amount { get; set; }

        public DateTime? date { get; set; }

        public DateTime? todate { get; set; }

        public bool ishistoric { get; set; }
    }

    

    //custom class to bind currency result for graph
    public class CurrencyResultSet
    {
        public DateTime Date { get; set; }

        public string SourceCurrency { get; set; }

        public string DestinationCurrency { get; set; }

        public string ConvertedAmount { get; set; }
    }

    //drop down items
    public class DropDownItems
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


}
