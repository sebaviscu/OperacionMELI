using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OperacionFuegoQuasar.Data;
using OperacionFuegoQuasar.Logic;
using OperacionFuegoQuasar.Model;
using System.Collections.Generic;

namespace OperacionFuegoQuasar.Tests
{
    [TestClass]
    public class CoordinatesUT
    {
        [TestMethod]
        public void Correct_Coordinates()
        {
            var mockSatelitesrepo = new Mock<ISatelitesRepository>();
            var mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var sateliteA = new Satelite()
            {
                Name = "A",
                Coordinates = new Coordinates(-3, 2)
            };
            var sateliteB = new Satelite()
            {
                Name = "B",
                Coordinates = new Coordinates(4, 2)
            };
            var sateliteC = new Satelite()
            {
                Name = "C",
                Coordinates = new Coordinates(1, -3)
            };
            mockSatelitesrepo.Setup(a => a.GetByName("A")).Returns(sateliteA);
            mockSatelitesrepo.Setup(a => a.GetByName("B")).Returns(sateliteB);
            mockSatelitesrepo.Setup(a => a.GetByName("C")).Returns(sateliteC);

            var expectedCoordinates = new Coordinates(1, 2);

            var signalList = new List<Signal>()
            {
                new Signal(){ Name = "A", Distance= 4},
                new Signal(){ Name = "B", Distance= 3},
                new Signal(){ Name = "C", Distance= 5},
            };

            var actual = galaxyManager.GetLocation(signalList);

            Assert.IsTrue(expectedCoordinates.Equals(actual));
        }

        [TestMethod]
        public void Send_Two_Signals()
        {
            var mockSatelitesrepo = new Mock<ISatelitesRepository>();
            var mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var signalList = new List<Signal>()
            {
                new Signal(){ Name = "A", Distance= 4},
                new Signal(){ Name = "B", Distance= 3},
            };

            var localitation = galaxyManager.GetLocation(signalList);

            Assert.IsTrue(localitation == null);
        }

        [TestMethod]
        public void Signal_Name_Empty()
        {
            var mockSatelitesrepo = new Mock<ISatelitesRepository>();
            var mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var signalList = new List<Signal>()
            {
                new Signal(){ Name = "A", Distance= 4},
                new Signal(){ Name = "B", Distance= 3},
                new Signal(){ Name = "", Distance= 7}
            };

            var localitation = galaxyManager.GetLocation(signalList);

            Assert.IsTrue(localitation == null);
        }

        [TestMethod]
        public void Error_Satelite_Name()
        {
            var mockSatelitesrepo = new Mock<ISatelitesRepository>();
            var mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var sateliteA = new Satelite()
            {
                Name = "A",
                Coordinates = new Coordinates(-3, 2)
            };
            var sateliteB = new Satelite()
            {
                Name = "B",
                Coordinates = new Coordinates(4, 2)
            };
            var sateliteC = new Satelite()
            {
                Name = "C",
                Coordinates = new Coordinates(1, -3)
            };
            mockSatelitesrepo.Setup(a => a.GetByName("A")).Returns(sateliteA);
            mockSatelitesrepo.Setup(a => a.GetByName("B")).Returns(sateliteB);
            mockSatelitesrepo.Setup(a => a.GetByName("C")).Returns(sateliteC);

            var signalList = new List<Signal>()
            {
                new Signal(){ Name = "A", Distance= 4},
                new Signal(){ Name = "B", Distance= 3},
                new Signal(){ Name = "D", Distance= 5},
            };

            var actual = galaxyManager.GetLocation(signalList);

            Assert.IsTrue(actual == null);
        }

    }
}
