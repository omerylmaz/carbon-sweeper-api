﻿using Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICalculationService
    {
        Task<decimal> CalculateFootPrint(GetCalculationRequest getCalculationRequest);
    }
}
