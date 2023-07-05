using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Response
{
    public class GetUsersWelcomeDataResponse
    {
        public int TotalUserAmount { get; set; }
        public int SavedTrees { get; set; }
        public decimal TotalFootPrint { get; set; }
    }
}
