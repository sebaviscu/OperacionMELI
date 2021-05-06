using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperacionFuegoQuasar.Logic;
using System.Collections.Generic;

namespace OperacionFuegoQuasar.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var l = new List<string[]>();
            l.Add(new string[] { "hola", "", "", "" });
            l.Add(new string[] { "", "chau", "", "" });
            l.Add(new string[] { "", "", "", "seba" });
            l.Add(new string[] { "", "", "loco", "" });

            //glaxyManager.GetMessage(l);

        }
    }
}
