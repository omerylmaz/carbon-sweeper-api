using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Response
{
    public class GetUserFootPrintResponse
    {
        public string FullName { get; set; }
        private decimal footPrint;

        public decimal FootPrint
        {
            get { return footPrint; }
            set { footPrint = value / 1000; }
        }

    }
}
