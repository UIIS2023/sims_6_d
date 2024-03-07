using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class RequestChartData
    {
        public string  Date { get; set; }
        public int NumberOfRequests { get; set; }

        public RequestChartData()
        {
            Date = string.Empty;
            NumberOfRequests = 0;
        }

        public RequestChartData(string date,int number)
        {
            Date = date;
            NumberOfRequests = number;
        }
    }
}
