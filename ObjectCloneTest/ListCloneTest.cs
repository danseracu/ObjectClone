using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCloneTest.Models;
using ObjectClone;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace ObjectCloneTest
{
    [TestClass]
    public class ListCloneTest : TestBase
    {

        [TestMethod]
        public void MultipleShallowObjects_CloneSuccesful()
        {
            var addresses = new List<Address>
            {
                new Address { AddressName = "Address1", Apartment = 1, Id = Guid.NewGuid() },
                new Address { AddressName = "Address2", Apartment = 2, Id = Guid.NewGuid() }
            };

            var clone = addresses.DeepCloneList<Address, List<Address>>();

            ValidateObjects(addresses, clone);
        }

        [TestMethod]
        public void MultipleComplexObjects_CloneSuccesful()
        {
            var users = new List<User>();

            for (int i = 0; i < 2; i++)
            {
                users.Add(new User {
                    Address = new Address { AddressName = $"Address{i}", Apartment = i, Id = Guid.NewGuid() },
                    LoginInfo = new Login($"user{i}", "pass"),
                    Name = $"name{i}"
                });
            }

            ValidateObjects(users, users.DeepCloneList<User, List<User>>());
        }

        [TestMethod]
        public void MultipleComplexObjects_WithLists_CloneSuccesful()
        {
            var reports = new List<DayReport>();

            for (int i = 0; i < 2; i++)
            {
                var report = new DayReport
                {
                    ActiveUsers = new List<User>(),
                    AdminLogin = new Login("username", "password"),
                    Url = "url"
                };
                for (int j = 0; j < 2; j++)
                {
                    report.ActiveUsers.Add(new User
                    {
                        Address = new Address { AddressName = $"Address{j}", Apartment = j, Id = Guid.NewGuid() },
                        LoginInfo = new Login($"user{j}", "pass"),
                        Name = $"name{j}"
                    });
                }
                reports.Add(report);
            }

            ValidateObjects(reports, reports.DeepCloneList<DayReport, List<DayReport>>());
        }

    }
}
