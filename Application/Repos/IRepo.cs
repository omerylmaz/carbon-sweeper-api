using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repos
{
    public interface IRepo<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
