﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant.Contracts.DTOs
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vänligen ange status")]
        [MaxLength(20, ErrorMessage = "Tecken är mer än 20 långa.")]
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
