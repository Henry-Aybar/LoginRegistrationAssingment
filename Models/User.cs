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
        public string FirstName {get;set;}

        [Required]
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