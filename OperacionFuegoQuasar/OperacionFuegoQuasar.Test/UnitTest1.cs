using NUnit.Framework;
using OperacionFuegoQuasar.Logic;
using System.Collections.Generic;

namespace OperacionFuegoQuasar.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1(GalaxyManager glaxyManager)
        {

            var l = new List<string[]>();
            l.Add(new string[] { "hola", "", "", "" });
            l.Add(new string[] { "", "chau", "", "" });
            l.Add(new string[] { "", "", "", "seba" });
            l.Add(new string[] { "", "", "loco", "" });

            glaxyManager.GetMessage(l);
            Assert.Pass();
        }
    }
}