﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizifyWeb.Models
{
    public class Team
    {
        public int Id { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public string Name { get; set; }
    }
}
