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

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object);

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

            var galaxyManager = new GalaxyManager(mockSatelitesrepo.Object, mokSignalRepo.Object);

            var messageList = new List<string[]>();
            messageList.Add(new string[] { "hola", "", "", "" });
            messageList.Add(new string[] { "hola", "", "", "" });
            messageList.Add(new string[] { "", "", "", "seba" });
            messageList.Add(new string[] { "", "", "test", "" });

            var message = galaxyManager.GetMessage(messageList);

            Assert.IsTrue(message == null);
        }
    }
}
