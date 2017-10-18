using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCloneTest
{
    public abstract class TestBase
    {

        protected void ValidateObjects(object objectA, object objectB)
        {
            Assert.AreEqual(JsonConvert.SerializeObject(objectA), JsonConvert.SerializeObject(objectB));
            Assert.AreNotEqual(objectA.GetHashCode(), objectB.GetHashCode());
            Assert.IsFalse(ReferenceEquals(objectA, objectB));
        }

    }
}
