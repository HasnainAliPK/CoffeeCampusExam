﻿using System;

namespace CoffeeCampus.Models
{
    public class ServiceLog //Service Log bog
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceBy { get; set; }
    }
}