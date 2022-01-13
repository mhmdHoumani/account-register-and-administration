using ASPTutorial.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTutorial.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = "The Name field must not exceed 30 characters.")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public IFormFile Image { get; set; }
    }
}
