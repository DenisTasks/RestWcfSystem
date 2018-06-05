using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BLL.BLLService;
using BLL.EntitesDTO;
using BLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace OutlookService.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestBll()
        {
            var test = new OutlookService(new StandardKernel());

        }
    }

    [TestClass]
    public class OutlookServiceTests
    {
        //[TestMethod]
        //public void TestTDD()
        //{
        //    var mockNinject = new Mock<IKernel>();
        //    var mockBll = new Mock<IBllServiceMain>();
        //    mockBll.Setup(s => s.GetAppointmentsByUserId(1))
        //        .Returns(new List<AppointmentDTO>
        //        {
        //            new AppointmentDTO {AppointmentId = 1, Subject = "test1"},
        //            new AppointmentDTO {AppointmentId = 2, Subject = "test2"},
        //            new AppointmentDTO {AppointmentId = 3, Subject = "test3"},
        //            new AppointmentDTO {AppointmentId = 4, Subject = "test4"},
        //            new AppointmentDTO {AppointmentId = 5, Subject = "test5"}
        //        });

        //    mockNinject.Setup(s => s.Get<IBllServiceMain>()).Returns(mockBll.Object);

        //    var service = new OutlookService(mockNinject.Object);

        //    var result = service.GetAppointments().Count;

        //    Assert.AreEqual(5, result);
        //}

        //[TestMethod]
        //public void Get_ShouldReturnAllApps()
        //{
        //    var mockBLL = new Mock<BllServiceMain>();
        //    mockBLL.Setup(s => s.GetAppointmentsByUserId(1)).Returns(new List<AppointmentDTO>
        //                {
        //                    new AppointmentDTO { AppointmentId = 1, Subject = "test1" },
        //                    new AppointmentDTO { AppointmentId = 2, Subject = "test2" },
        //                    new AppointmentDTO { AppointmentId = 3, Subject = "test3" },
        //                    new AppointmentDTO { AppointmentId = 4, Subject = "test4" },
        //                    new AppointmentDTO { AppointmentId = 5, Subject = "test5" }
        //                });
        //    var service = new OutlookService(mockBLL.Object);
        //    var result = service.GetAppointments().Count;

        //    Assert.AreEqual(5, result);
        //}

        //[TestMethod]
        //public void Get_ShouldReturnCorrectApp()
        //{
        //    var testApp = new AppointmentDTO { AppointmentId = 4, Subject = "test4" };
        //    var mockBLL = new Mock<IBllServiceMain>();
        //    mockBLL.Setup(s => s.GetAppointmentById(4)).Returns(new AppointmentDTO
        //    {
        //        AppointmentId = 4,
        //        Subject = "test4"
        //    });
        //    var service = new OutlookService(mockBLL.Object);

        //    var result = service.GetAppointmentById(4);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(testApp.Subject, result.Subject);
        //}

        //[TestMethod]
        //public void Get_ShouldReturnAllScrollApp()
        //{
        //    var mockBLL = new Mock<IBllServiceMain>();
        //    mockBLL.Setup(s => s.GetAppointmentsByUserIdSqlText(1, 10, 5)).Returns(new List<AppointmentDTO>
        //    {
        //        new AppointmentDTO { AppointmentId = 1, Subject = "test1" },
        //        new AppointmentDTO { AppointmentId = 2, Subject = "test2" },
        //        new AppointmentDTO { AppointmentId = 3, Subject = "test3" },
        //        new AppointmentDTO { AppointmentId = 4, Subject = "test4" },
        //        new AppointmentDTO { AppointmentId = 5, Subject = "test5" }
        //    });
        //    var service = new OutlookService(mockBLL.Object);

        //    var result = service.GetAppointmentsWithSql(1, 10, 5).Count;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(5, result);
        //}

        //[TestMethod]
        //public void Get_ShouldNotFindProduct()
        //{
        //    var mockBLL = new Mock<IBllServiceMain>();
        //    mockBLL.Setup(s => s.GetAppointmentById(4)).Returns(new AppointmentDTO
        //    {
        //        AppointmentId = 4,
        //        Subject = "test4"
        //    });

        //    var service = new OutlookService(mockBLL.Object);

        //    var result = service.GetAppointmentById(999);

        //    Assert.IsNull(result);
        //}

        //[TestMethod]
        //public void Post_CanCreateAppointment()
        //{
        //    var mockBLL = new Mock<IBllServiceMain>();
        //    var newApp = new AppointmentDTO
        //    {
        //        Subject = "test"
        //    };
        //    var outputApp = new AppointmentDTO
        //    {
        //        AppointmentId = 5,
        //        Subject = "test"
        //    };
        //    mockBLL.Setup(s => s.AddAppointmentWeb(newApp, 1)).Returns(outputApp);

        //    var service = new OutlookService(mockBLL.Object);

        //    var result = service.AddAppointment(newApp, 1);

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.Subject, "test");
        //    Assert.AreEqual(result.AppointmentId, 5);
        //}

        //[TestMethod]
        //public void Put_CanUpdateAppointment()
        //{
        //    var mockBLL = new Mock<IBllServiceMain>();
        //    var updateApp = new AppointmentDTO
        //    {
        //        AppointmentId = 1,
        //        Subject = "after"
        //    };
        //    var notUpdatedApp = new AppointmentDTO
        //    {
        //        AppointmentId = 1,
        //        Subject = "before"
        //    };
        //    var updatedApp = new AppointmentDTO
        //    {
        //        AppointmentId = 1,
        //        Subject = "after"
        //    };

        //    mockBLL.Setup(s => s.UpdateAppointment(updateApp)).Returns(updatedApp);
        //    mockBLL.Setup(s => s.GetAppointmentById(1)).Returns(notUpdatedApp);

        //    var service = new OutlookService(mockBLL.Object);

        //    var beforeUpdate = service.GetAppointmentById(1);
        //    var afterUpdate = service.UpdateAppointment(updateApp);

        //    Assert.AreEqual(updateApp.AppointmentId, notUpdatedApp.AppointmentId);
        //    Assert.AreNotEqual(updateApp.Subject, beforeUpdate.Subject);
        //    Assert.AreNotSame(updateApp, notUpdatedApp);

        //    Assert.AreNotEqual(beforeUpdate.Subject, afterUpdate.Subject);

        //    Assert.AreEqual(updateApp.AppointmentId, afterUpdate.AppointmentId);
        //    Assert.AreEqual(updateApp.Subject, afterUpdate.Subject);
        //    Assert.AreNotSame(updateApp, afterUpdate);
        //    Assert.IsNotNull(afterUpdate);
        //}

        //[TestMethod]
        //public void Delete_CanRemoveAppointment()
        //{
        //    var mockBLL = new Mock<IBllServiceMain>();
        //    var appointment = new AppointmentDTO
        //    {
        //        AppointmentId = 1
        //    };
        //    var successRemovedApp = new AppointmentDTO
        //    {
        //        AppointmentId = 1
        //    };

        //    mockBLL.Setup(s => s.RemoveAppointment(1)).Returns(successRemovedApp);
        //    mockBLL.Setup(s => s.GetAppointmentById(1));

        //    var service = new OutlookService(mockBLL.Object);

        //    var successResult = service.RemoveAppointmentById(1);
        //    var failResult = service.GetAppointmentById(1);

        //    Assert.IsNotNull(successResult);
        //    Assert.AreNotSame(appointment, successResult);
        //    Assert.AreEqual(appointment.AppointmentId, successResult.AppointmentId);

        //    Assert.IsNull(failResult);
        //}
    }
}