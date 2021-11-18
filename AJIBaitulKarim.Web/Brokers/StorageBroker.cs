using AJIBaitulKarim.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AJIBaitulKarim.Web.Brokers
{
    public class StorageBroker : DbContext, IStorageBroker
    {
        private DbSet<Student> Students { get; set; }

        public async Task AddStudentAsync(Student student)
        {
            try
            {
                await this.Students.AddAsync(student);
                await this.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw dbUpdateException;
            }
        }
    }
}
