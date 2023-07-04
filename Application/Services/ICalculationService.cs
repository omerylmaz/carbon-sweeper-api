using Application.Dto.Request;
using Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICalculationService
    {
        Task<decimal> CalculateFootPrintAsync(GetCalculationRequest getCalculationRequest);
        Task<GetFootPrintWarningListResponse> GetFootPrintWarnings(int userId);
    }
}
