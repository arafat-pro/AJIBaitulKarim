using AJIBaitulKarim.Web.Brokers;
using AJIBaitulKarim.Web.Models;
using AJIBaitulKarim.Web.Models.Exceptions;
using AJIBaitulKarim.Web.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace AJIBaitulKarim.Web.Test
{
    public class StudentsServiceTests
    {
        private Mock<IStorageBroker> storageBrokerMock;
        public StudentsServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
        }

        [Fact]
        public async void ShouldPersistStudentWhenStudentIsPassedIn()
        {
            //given            
            Student student = new Filler<Student>().Create();

            storageBrokerMock.Setup(broker => broker.AddStudentAsync(student));

            //when
            var studentsService = new StudentsService(storageBrokerMock.Object);

            await studentsService.RegisterStudentAsync(student);

            //then
            storageBrokerMock.Verify(broker => broker.AddStudentAsync(student), Times.Once);
        }

        [Fact]
        public async void ShouldThrowStudentRegistrationExceptionWhenStorageFails()
        {
            //given
            Student student = new Filler<Student>().Create();
            DbUpdateException dbUpdateException = CreateNewDbUpdateException();

            this.storageBrokerMock.Setup(broker =>
                broker.AddStudentAsync(student))
                    .ThrowsAsync(dbUpdateException);

            //when
            var studentsService = new StudentsService(this.storageBrokerMock.Object);

            Task registerStudentTask = studentsService.RegisterStudentAsync(student);


            //then
            await Assert.ThrowsAsync<StudentRegistrationFailedException>(
                () => registerStudentTask);

        }

        private static DbUpdateException CreateNewDbUpdateException()
        {
            return new DbUpdateException(
                message: new MnemonicString().GetValue(),
                innerException: new Exception());
        }
    }
}