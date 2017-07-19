using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Data.Domain;
using Data.Repositories.Abstract;
using Web.Controllers;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    class Calendar
    {
        [Test]
        public void Can_Get_Events_By_DateRange()
        {
            // Arrange
            ICalendarEventRepository repo = UnitTestHelpers.MockEventsRepository(
                //new CalendarEvent { title = "meeting", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(3) },
                //new CalendarEvent { title = "meeting 2", StartTime = DateTime.Now.AddDays(10), EndTime = DateTime.Now.AddHours(3) }
            );
            //var controller = new CalendarController(repo);
            
            // Act
            //var actual = controller.GetEvents(UnitTestHelpers.ToUnixTimespan(DateTime.Now.AddDays(-1)), 
              //  UnitTestHelpers.ToUnixTimespan(DateTime.Now.AddDays(7))) as JsonResult;
            //List<CalendarDTO> result = actual.Data as List<CalendarDTO>;

            // Assert
            //Assert.AreEqual(1, result.Count);
            

        }

        [Test]
        public void Can_Get_Events()
        {
            // Arrange
            ICalendarEventRepository repo = UnitTestHelpers.MockEventsRepository(
                //new CalendarEvent { title = "meeting", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(3) },
                //new CalendarEvent { title = "meeting 2", StartTime = DateTime.Now.AddDays(10), EndTime = DateTime.Now.AddHours(3) }
            );
            //var controller = new CalendarController(repo);

            // Act
            //var actual = controller.GetEvents(UnitTestHelpers.ToUnixTimespan(DateTime.Now.AddDays(-1)),
                //UnitTestHelpers.ToUnixTimespan(DateTime.Now.AddDays(7))) as JsonResult;
            //List<CalendarDTO> result = actual.Data as List<CalendarDTO>;

            // Assert
            //Assert.AreEqual(2, result.Count);


        }
    }
}
