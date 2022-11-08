using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRegistration.Domain.Models
{
    public class AccountModel : DomainObjects
    {
        public UserModel AccountHolder { get; set; }
        public double Balance { get; set; }
    }
}
