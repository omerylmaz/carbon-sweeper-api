using Application.Dto.Response;
using Application.Repos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IReadRepo<User> _userReadRepo;
        private readonly IMapper _mapper;
        public UserService(
            IReadRepo<User> userReadRepo,
            IMapper mapper
            )
        {
            _userReadRepo = userReadRepo;
            _mapper = mapper;
        }
        public async Task<GetUserFootPrintsListResponse> GetUsersFootPrintAsync()
        {
            var usersDb = await _userReadRepo.GetAll().OrderByDescending(x => x.FootPrint).ToListAsync();
            var usersResponse = _mapper.Map<List<GetUserFootPrintResponse>>(usersDb);
            return new GetUserFootPrintsListResponse() { UserFootPrintList = usersResponse };
        }
    }
}
