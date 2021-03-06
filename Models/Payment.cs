﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoulaChallenge.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }

        // Foreign Key
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}