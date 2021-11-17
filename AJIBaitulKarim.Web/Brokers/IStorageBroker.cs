using AJIBaitulKarim.Web.Models;
using System.Threading.Tasks;

namespace AJIBaitulKarim.Web.Brokers
{
    public interface IStorageBroker
    {
        Task AddStudentAsync (Student student);
    }
}
