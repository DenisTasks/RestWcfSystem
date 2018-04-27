//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using BLL.EntitesDTO;
//using BLL.Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Model.Entities;
//using Moq;
//using WebApiNET.Controllers;

//namespace WebApiNET.Tests
//{
//    [TestClass]
//    public class TestScrollController
//    {
//        [TestMethod]
//        public void Get_ShouldReturnAllApps()
//        {
//            var mockBLL = new Mock<IBLLServiceMain>();
//            // 10 => Get(2) * PageSize
//            mockBLL.Setup(s => s.GetAppointmentsByUserIdSqlText(1, 10, 5)).Returns(new List<AppointmentDTO>
//            {
//                new AppointmentDTO { AppointmentId = 1, Subject = "test1" },
//                new AppointmentDTO { AppointmentId = 2, Subject = "test2" },
//                new AppointmentDTO { AppointmentId = 3, Subject = "test3" },
//                new AppointmentDTO { AppointmentId = 4, Subject = "test4" },
//                new AppointmentDTO { AppointmentId = 5, Subject = "test5" }
//            });

//            var controller = new ScrollController
//            {
//                Request = new HttpRequestMessage(),
//                Configuration = new HttpConfiguration(),
//                PageSize = 5
//            };

//            var result = controller.Get(2).Content.ReadAsAsync<List<AppointmentDTO>>().Result.Count;

//            Assert.AreEqual(5, result);
//        }

//        //[TestMethod]
//        //public async Task GetAsync_ShouldReturnAllApps()
//        //{
//        //    var testApps = GetTestApps().Count;
//        //    var mock = new Mock<IBLLServiceMain>();
//        //    mock.Setup(s => s.GetAppointmentsByUserIdSqlText(1, 10, 5)).Returns(new List<AppointmentDTO>
//        //    {
//        //        new AppointmentDTO { AppointmentId = 1, Subject = "test1" },
//        //        new AppointmentDTO { AppointmentId = 2, Subject = "test2" },
//        //        new AppointmentDTO { AppointmentId = 3, Subject = "test3" },
//        //        new AppointmentDTO { AppointmentId = 4, Subject = "test4" },
//        //        new AppointmentDTO { AppointmentId = 5, Subject = "test5" }
//        //    });

//        //    var controller = new ScrollController(mock.Object)
//        //    {
//        //        Request = new HttpRequestMessage(),
//        //        Configuration = new HttpConfiguration(),
//        //        PageSize = 5
//        //    };

//        //    var result = await controller.GetAsync(2);
//        //    var resultCount = result.Content.ReadAsAsync<List<AppointmentDTO>>().Result.Count;

//        //    Assert.AreEqual(testApps, resultCount);
//        //}
//    }
//}
