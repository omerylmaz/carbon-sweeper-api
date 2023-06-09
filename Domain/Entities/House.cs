﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class House : BaseEntity
    {
        public decimal? Electricity { get; set; }
        public decimal? Coal { get; set; }
        public decimal? LPG { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
