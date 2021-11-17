using AJIBaitulKarim.Web.Brokers;
using AJIBaitulKarim.Web.Models;
using System.Threading.Tasks;

namespace AJIBaitulKarim.Web.Services
{
    public class StudentsService : IStudentsService
    {

        readonly IStorageBroker storageBroker;
        public StudentsService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public Task RegisterStudentAsync(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}