using FCCodingChallenge.API.Data.ViewModels;
using Newtonsoft.Json;
using System;

namespace FCCodingChallenge.API.Data.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageURL { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
