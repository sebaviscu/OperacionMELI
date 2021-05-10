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
    public class MessagesUT
    {
        [TestMethod]
        public void Correct_Message()
        {
            Mock<ISatelitesRepository> mockSatelitesrepo = new Mock<ISatelitesRepository>();
            Mock<ISignalRepository> mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var messageList = new List<string[]>();
            messageList.Add(new string[] { "hola", "", "", "" });
            messageList.Add(new string[] { "", "chau", "", "" });
            messageList.Add(new string[] { "", "", "", "seba" });
            messageList.Add(new string[] { "", "", "test", "" });

            var message = galaxyManager.GetMessage(messageList);

            Assert.AreEqual("hola chau test seba", message);
        }

        [TestMethod]
        public void Incompleted_Message()
        {
            Mock<ISatelitesRepository> mockSatelitesrepo = new Mock<ISatelitesRepository>();
            Mock<ISignalRepository> mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var messageList = new List<string[]>();
            messageList.Add(new string[] { "este", "", "", "" });
            messageList.Add(new string[] { "este", "", "", "" });
            messageList.Add(new string[] { "", "", "", "mensaje" });
            messageList.Add(new string[] { "", "", "un", "" });

            var message = galaxyManager.GetMessage(messageList);

            Assert.IsTrue(message == null);
        }

        [TestMethod]
        public void Empty_Message()
        {
            Mock<ISatelitesRepository> mockSatelitesrepo = new Mock<ISatelitesRepository>();
            Mock<ISignalRepository> mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var messageList = new List<string[]>();
            var message = galaxyManager.GetMessage(messageList);

            Assert.IsTrue(message == null);
        }

        [TestMethod]
        public void Get_Signals_For_Message_Not_Satelites()
        {
            Mock<ISatelitesRepository> mockSatelitesrepo = new Mock<ISatelitesRepository>();
            Mock<ISignalRepository> mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            galaxyManager.SaveSignal(new Signal() { Name = "A", Distance = 4 });

            var message = galaxyManager.GetSignalsForMessage();

            Assert.IsTrue(message == null);
        }

        [TestMethod]
        public void Get_Signals_For_Message_Less_Than_Three_Satelites()
        {
            Mock<ISatelitesRepository> mockSatelitesrepo = new Mock<ISatelitesRepository>();
            Mock<ISignalRepository> mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var signalList = new List<Signal>()
            {
                new Signal(){ Name = "A", Distance= 4},
                new Signal(){ Name = "B", Distance= 3},
            };

            mokSignalRepo.Setup(a => a.GetAll()).Returns(signalList);

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var message = galaxyManager.GetSignalsForMessage();

            Assert.IsTrue(message == null);
        }

        [TestMethod]
        public void Get_Signals_For_Message()
        {
            Mock<ISatelitesRepository> mockSatelitesrepo = new Mock<ISatelitesRepository>();
            Mock<ISignalRepository> mokSignalRepo = new Mock<ISignalRepository>();
            var mokLogger = new Mock<ILogger<GalaxyManager>>();

            var ExpectedSignals = new List<Signal>()
            {
                new Signal(){ Name = "A", Distance= 4},
                new Signal(){ Name = "B", Distance= 3},
                new Signal(){ Name = "C", Distance= 7},
            };

            mokSignalRepo.Setup(a => a.GetAll()).Returns(ExpectedSignals);

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object, mokLogger.Object);

            var signalsList = galaxyManager.GetSignalsForMessage();

            Assert.IsTrue(signalsList.Count == 3);
        }

    }
}
