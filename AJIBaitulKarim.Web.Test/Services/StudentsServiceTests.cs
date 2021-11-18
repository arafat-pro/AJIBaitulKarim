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
        private Mock<ILoggingBroker> loggingBrokerMock;

        public StudentsServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
        }

        [Fact]
        public async void ShouldPersistStudentWhenStudentIsPassedIn()
        {
            //given            
            Student student = new Filler<Student>().Create();

            this.storageBrokerMock.Setup(broker => broker.AddStudentAsync(student));

            //when
            var studentsService = new StudentsService(
                this.storageBrokerMock.Object,
                this.loggingBrokerMock.Object
                );

            await studentsService.RegisterStudentAsync(student);

            //then
            this.storageBrokerMock.Verify(broker => broker.AddStudentAsync(student), Times.Once);
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
            var studentsService = new StudentsService(this.storageBrokerMock.Object, this.loggingBrokerMock.Object);

            Task registerStudentTask = studentsService.RegisterStudentAsync(student);


            //then
            await Assert.ThrowsAsync<StudentRegistrationFailedException>(
                () => registerStudentTask);

            this.loggingBrokerMock.Verify(broker => broker.Error(dbUpdateException.Message), Times.Once);

        }

        private static DbUpdateException CreateNewDbUpdateException()
        {
            return new DbUpdateException(
                message: new MnemonicString().GetValue(),
                innerException: new Exception());
        }
    }
}