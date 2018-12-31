using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembershipApplication.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "You must provide your name !!")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters !!")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide your age !!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Age must be a number !!")]
        [Range(20, 60 , ErrorMessage = "You must have age between 20 to 60 !!")]
        [Display(Name = "Age")]
        public string Age { get; set; }

        [Required(ErrorMessage = "You must select your gender !!")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "You must provide a Phone Number !!")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must provide an address !!")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must provide a Email !!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public string MembershipNumber { get; set; }

        public User()
        {
            IsAdmin = false;
        }
    }
}