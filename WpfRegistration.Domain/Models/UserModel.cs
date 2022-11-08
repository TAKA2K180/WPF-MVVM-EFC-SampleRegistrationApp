using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRegistration.Domain.Models
{
    public class UserModel : DomainObjects
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateJoined { get; set; }
        public string PasswordHash { get; set; }
        public bool isVaccinated { get; set; }
        public string VaccineName { get; set; }
        public bool isBoosterShot { get; set; }
        public int NumberofShots { get; set; }
        public string QrCode { get; set; }
        public DateTime DateFirstDose { get; set; }
    }
}
