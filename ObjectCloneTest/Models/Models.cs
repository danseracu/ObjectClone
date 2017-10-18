using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCloneTest.Models
{
    public class Address
    {
        public string AddressName { get; set; }
        public int Apartment { get; set; }
        public Guid Id { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class User
    {
        public Address Address { get; set; }
        public string Name { get; set; }
        public Login LoginInfo { get; set; }
    }

    public class DayReport
    {
        public List<User> ActiveUsers { get; set; }
        public string Url { get; set; }
        public Login AdminLogin{ get; set; }
    }
}
