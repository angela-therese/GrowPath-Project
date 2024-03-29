﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowPath.Models;

namespace GrowPath.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ClassId { get; set; }
        public string ImageUrl { get; set; }
        public List<Goal> Goals { get; set; }
    }
}
