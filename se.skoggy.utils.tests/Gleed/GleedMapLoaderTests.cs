using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using se.skoggy.utils.Gleed;

namespace se.skoggy.utils.tests.Gleed
{
    [TestClass]
    public class GleedMapLoaderTests
    {
        [TestMethod]
        [DeploymentItem("TestFiles/ValidGleedMap.xml", "TestFiles")]
        public void WHAT_WHEN_THEN()
        {
            var loader = new GleedMapLoader();
            const string fileName = "TestFiles/ValidGleedMap.xml";
            try
            {
                var content = loader.Load(fileName);
                

            }
            catch
            {
                Assert.Fail("should work");
            }
        }
    }
}
