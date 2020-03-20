﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.Models
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime PaidDate { get; set; }
        [Required]
        public bool Canceled { get; set; }
        [Required]
        public bool Declined { get; set; }
    }
}