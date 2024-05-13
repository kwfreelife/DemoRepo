using DemoApiApp.Model;
using DemoApiApp.Repository;

namespace DemoApiApp.Interface
{
    public interface IUserRepository : IGenericRepository<UserDemo>
    {
        //Here, you need to define the operations which are specific to Employee Entity
        IEnumerable<UserDemo> GetByName(string nameToFind);

        string DeleteById(int Id);
    }
}
