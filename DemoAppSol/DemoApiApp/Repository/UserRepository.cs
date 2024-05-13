using DemoApiApp.Interface;
using DemoApiApp.Model;

namespace DemoApiApp.Repository
{
    public class UserRepository : GenericRepository<UserDemo>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public UserRepository(DataContext _context) : base(_context)
        {
        }

        public IEnumerable<UserDemo> GetByName(string nameToFind)
        {
            return this.table.Where(p => p.UserName == nameToFind).ToList();
        }

        public string DeleteById(int id)
        {
            UserDemo user = this.table.SingleOrDefault(p => p.Id == id);
            string retval = user.UserName;
            this.table.Remove(user);
            this._context.SaveChanges();
            return retval;
        }
    }
}
