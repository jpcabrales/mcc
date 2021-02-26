using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoulaChallenge.Models
{
    public class AccountDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public List<PaymentDTO> Payments { get; set; }
    }
}