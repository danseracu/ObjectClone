using System;
using ObjectCloneTest.Models;
using System.Collections.Generic;
using ObjectClone;
using System.Diagnostics;

namespace ObjectCloneTest
{
    //[TestClass]
    //public class StressTests : TestBase
    //{
    //    [TestMethod]
    //    [Ignore]
    //    public void IntricateLists_1000_500()
    //    {

    //        var reports = new List<DayReport>();

    //        for (int i = 1; i <= 1000; i++)
    //        {
    //            var report = new DayReport
    //            {
    //                ActiveUsers = new List<User>(),
    //                AdminLogin = new Login("username", "password"),
    //                Url = "url"
    //            };
    //            for (int j = 1; j <= 500; j++)
    //            {
    //                report.ActiveUsers.Add(new User
    //                {
    //                    Address = new Address { AddressName = $"Address{j}", Apartment = j, Id = Guid.NewGuid() },
    //                    LoginInfo = new Login($"user{j}", "pass"),
    //                    Name = $"name{j}"
    //                });
    //            }
    //            reports.Add(report);
    //        }
    //        var stopwatch = new Stopwatch();

    //        stopwatch.Start();

    //        var clone = reports.DeepCloneList<DayReport, List<DayReport>>();

    //        stopwatch.Stop();

    //        ValidateObjects(reports, clone);

    //        Assert.IsTrue(stopwatch.Elapsed.TotalSeconds <= 8);
    //    }
    //}
}
