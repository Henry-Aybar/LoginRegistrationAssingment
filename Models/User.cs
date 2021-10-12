using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistrationAssingment.Models
{
    public class User
    {
        //id, unique identifier (email, username), password, confirm password, CreatedAt, UpdatedAt, FirstName, LastName
        [Key]
        public int UserId {get;set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [MinLength(2, ErrorMessage="You Need at least 2 charaters in your First Name!")]
        public string FirstName {get;set;}

        [Required]
        [MinLength(2, ErrorMessage="You Need at least 8 charaters in your Last Name!")]
        public string LastName {get;set;}


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="You Need at least 8 charaters for the Password!")]
        public string Password {get;set;}

        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}