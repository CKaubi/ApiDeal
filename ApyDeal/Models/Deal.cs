using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApyDeal.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Urgency { get; set; }
    }
}