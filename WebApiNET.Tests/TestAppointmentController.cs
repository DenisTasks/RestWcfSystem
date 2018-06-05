//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Results;
//using BLL.EntitesDTO;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using WebApiNET.Controllers;
//using WebApiNET.ServiceReference;

//namespace WebApiNET.Tests
//{
//    [TestClass]
//    public class TestAppointmentController
//    {
//        [TestMethod]
//        public void Get_ShouldReturnCorrectApp()
//        {
//            var testApp = new AppointmentDTO { AppointmentId = 4, Subject = "test4" };
//            var mock = new Mock<IOutlookService>();
//            mock.Setup(s => s.GetAppointmentById(4)).Returns(new AppointmentDTO
//            {
//                AppointmentId = 4,
//                Subject = "test4"
//            });

//            var controller = new AppointmentController
//            {
//                Request = new HttpRequestMessage(),
//                Configuration = new HttpConfiguration()
//            };

//            var result = controller.Get(4) as OkNegotiatedContentResult<AppointmentDTO>;
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Content);
//            Assert.AreEqual(testApp.Subject, result.Content.Subject);
//        }

//        [TestMethod]
//        public void Get_ShouldNotFindProduct()
//        {
//            //var mockBLL = new Mock<IBLLServiceMain>();
//            //mockBLL.Setup(s => s.GetAppointmentById(4)).Returns(new AppointmentDTO
//            //{
//            //    AppointmentId = 4,
//            //    Subject = "test4"
//            //});

//            var controller = new AppointmentController
//            {
//                Request = new HttpRequestMessage(),
//                Configuration = new HttpConfiguration()
//            };

//            var result = controller.Get(999);

//            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//        }

//        [TestMethod]
//        public void Post_CanCreateAppointment()
//        {
//            //var mockBLL = new Mock<IBLLServiceMain>();
//            //mockBLL.Setup(s => s.GetAppointmentById(4)).Returns(new AppointmentDTO
//            //{
//            //    AppointmentId = 4,
//            //    Subject = "test4"
//            //});
//            //var mockLog = new Mock<ILogService>();
//            //var mockNotify = new Mock<INotifyService>();

//            var controller = new AppointmentController
//            {
//                Request = new HttpRequestMessage(),
//                Configuration = new HttpConfiguration()
//            };


//        }
//    }
//}
