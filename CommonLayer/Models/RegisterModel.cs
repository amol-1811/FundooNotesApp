using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class RegisterModel
    {
        [StringLength(20, MinimumLength =3, ErrorMessage ="First Name should be of minimum 3 characters.")]
        public string FirstName { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name should be of minimum 3 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter valid birthdate.")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other.")]
        public string Gender { get; set; }
        [EmailAddress(ErrorMessage ="Enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter valid password.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character.")]
        public string Password { get; set; }
    }
}
