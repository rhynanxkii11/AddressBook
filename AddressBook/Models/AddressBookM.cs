using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.Models
{
    public class AddressBookM
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [DisplayName("Contact Number")]
        [Required]
        public string ContactNumber { get; set; }
        [DisplayName("E-Mail")]
        [Required]
        public string Email { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [NotMapped]
        public string CompleteAddress
        {
            get
            {
                return Address + ", " + City + ", " + Province;
            }
        }
    }
}
