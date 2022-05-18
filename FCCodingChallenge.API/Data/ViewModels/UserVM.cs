using FCCodingChallenge.API.Data.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace FCCodingChallenge.API.Data.ViewModels
{
    public class UserVM 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public IFormFile Photo { get; set; }
        public string Role { get; set; }
    }


    public class UpdateUserRequest
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Role { get; set; }
    }
}
