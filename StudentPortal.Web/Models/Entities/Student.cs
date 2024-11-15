using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number must contain only numbers.")]
        public string PhoneNumber { get; set; }


        public bool Subscribed { get; set; }


    }
}
