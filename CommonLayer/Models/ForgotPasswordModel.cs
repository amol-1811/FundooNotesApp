using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class ForgotPasswordModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(50, ErrorMessage = "Email should not exceed 50 characters.")]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
