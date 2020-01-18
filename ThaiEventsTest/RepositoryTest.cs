using Microsoft.Extensions.Logging;
using Moq;
using System;
using ThaiEvents.Data;
using Xunit;

namespace ThaiEventsTest
{
    public class RepositoryTest
    {
        private Repository _repo;
       
        public RepositoryTest()
        {
            var logMock = new Mock<ILogger<Repository>>();
            _repo = new Repository(logMock.Object);
        }

        [Theory]
        [InlineData("Title1", "note1", null, null, true)]
        [InlineData("Title2", "note2", null, null, true)]
        public void CreateEvent(string title, string note, DateTime startDate, DateTime endDate, bool isAllDay)
        {
            var result = _repo.CreateEvent(title,note,startDate,endDate,isAllDay);
            Assert.Equal(result,true);
        }
    }
}
