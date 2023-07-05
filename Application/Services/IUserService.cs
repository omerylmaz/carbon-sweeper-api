using Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        public Task<GetUserFootPrintsListResponse> GetUsersFootPrintAsync();
        public Task<GetUsersWelcomeDataResponse> GetUsersWelcomeDataAsync();

    }
}
