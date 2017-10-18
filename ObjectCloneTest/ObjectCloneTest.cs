using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCloneTest.Models;
using ObjectClone;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ObjectCloneTest
{
    [TestClass]
    public class ObjectCloneTest : TestBase
    {
        [TestMethod]
        public void SingleShallowObject_CloneSuccesful()
        {

            var address = new Address
            {
                AddressName = "Addres123",
                Apartment = 3,
                Id = Guid.NewGuid()
            };

            var clone = address.DeepClone();

            ValidateObjects(address, clone);

        }

        [TestMethod]
        public void SingleComplexObject_CloneSuccesful()
        {
            var user = new User
            {
                Address = new Address { AddressName = "123", Apartment = 1, Id = Guid.NewGuid() },
                LoginInfo = new Login("user", "pass"),
                Name = "User1"
            };

            var clone = user.DeepClone();

            ValidateObjects(user, clone);
        }

        [TestMethod]
        public void SingleComplexObject_WithList_CloneSuccesful()
        {
            var report = new DayReport
            {
                AdminLogin = new Login("admin", "pass"),
                Url = "url",
                ActiveUsers = new List<User>()
            };

            for (int i = 0; i < 2; i++)
            {
                report.ActiveUsers.Add(new User
                {
                    Address = new Address { AddressName = $"Address{i}", Apartment = i, Id = Guid.NewGuid() },
                    LoginInfo = new Login($"user{i}", "pass"),
                    Name = $"name{i}"
                });
            }

            var clone = report.DeepClone();

            ValidateObjects(report, clone);
        }
    }
}
