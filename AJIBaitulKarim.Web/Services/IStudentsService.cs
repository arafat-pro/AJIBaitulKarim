using AJIBaitulKarim.Web.Models;
using System.Threading.Tasks;

namespace AJIBaitulKarim.Web.Services
{
    public interface IStudentsService
    {
        Task RegisterStudentAsync(Student student);
    }
}