using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using NUnit.Framework;
using Moq;
using Data.Domain;

namespace UnitTesting
{
    public static class UnitTestHelpers
    {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }

        #region Calendar
        public static ICalendarEventRepository MockEventsRepository(params CalendarEvent[] events)
        {
            // Generate an implementer of Icalendarevents at runtime using Moq
            var mockEventsRepos = new Mock<ICalendarEventRepository>();
            mockEventsRepos.Setup(x => x.CalendarEvents).Returns(events.AsQueryable());
            return mockEventsRepos.Object;
        }

        public static long ToUnixTimespan(DateTime date)
        {
            TimeSpan tspan = date.ToUniversalTime().Subtract(
             new DateTime(1970, 1, 1, 0, 0, 0));

            return (long)Math.Truncate(tspan.TotalSeconds);
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        #endregion
    }
}
