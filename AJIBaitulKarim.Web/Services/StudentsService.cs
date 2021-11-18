using AJIBaitulKarim.Web.Brokers;
using AJIBaitulKarim.Web.Models;
using AJIBaitulKarim.Web.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AJIBaitulKarim.Web.Services
{
    public class StudentsService : IStudentsService
    {

        readonly IStorageBroker storageBroker;
        readonly ILoggingBroker loggingBroker;

        public StudentsService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async Task RegisterStudentAsync(Student student)
        {
            try
            {
                await this.storageBroker.AddStudentAsync(student);
            }
            catch (DbUpdateException)
            {
                throw new StudentRegistrationFailedException();
            }
        }
    }
}