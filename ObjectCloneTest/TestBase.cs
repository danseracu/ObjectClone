
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ObjectCloneTest
{
    public abstract class TestBase
    {

        protected void ValidateObjects(object objectA, object objectB)
        {
            Assert.Equal(JsonConvert.SerializeObject(objectA), JsonConvert.SerializeObject(objectB));
            Assert.NotEqual(objectA.GetHashCode(), objectB.GetHashCode());
            Assert.False(ReferenceEquals(objectA, objectB));
        }

    }
}
