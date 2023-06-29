using Application.Dto.Request;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class CalculationService : ICalculationService
    {
        public Task<decimal> CalculateFootPrint(GetCalculationRequest getCalculationRequest)
        {
            throw new NotImplementedException();
        }
    }
}
