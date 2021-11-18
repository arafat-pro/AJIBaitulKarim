using AJIBaitulKarim.Web.Brokers;
using AJIBaitulKarim.Web.Models;
using AJIBaitulKarim.Web.Services;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace AJIBaitulKarim.Web.Test
{
    public class StudentsServiceTests
    {
        [Fact]
        public async void ShouldPersistStudentWhenStudentIsPassedIn()
        {
            //given
            var storageBrokerMock = new Mock<IStorageBroker>();
            Student student = new Filler<Student>().Create();

            storageBrokerMock.Setup(broker => broker.AddStudentAsync(student));

            //when
            var studentsService = new StudentsService(storageBrokerMock.Object);

            await studentsService.RegisterStudentAsync(student);

            //then
            storageBrokerMock.Verify(broker => broker.AddStudentAsync(student), Times.Once);
        }
    }
}