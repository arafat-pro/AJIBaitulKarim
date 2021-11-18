using AJIBaitulKarim.Web.Brokers;
using AJIBaitulKarim.Web.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task RegisterStudentAsync(Student student)
        {
            try {
            await this.storageBroker.AddStudentAsync(student);            
            }
            catch(DbUpdateException dbUpdateException)
            {
                throw dbUpdateException;
            }
        }
    }
}