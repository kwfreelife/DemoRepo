using DemoApiApp.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DemoApiApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        //The following variable is going to hold the DBContext instance
        protected DataContext _context = null;
        //The following Variable is going to hold the DbSet Entity
        protected DbSet<T> table = null;
        
        protected readonly IConfiguration Configuration;
        //Using the Parameterless Constructor, 
        //we are initializing the context object and table variable
        public GenericRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this._context = new DataContext(Configuration);
            //Whatever class name we specify while creating the instance of GenericRepository
            //That class name will be stored in the table variable
            table = _context.Set<T>();
        }

        //Using the Parameterized Constructor, 
        //we are initializing the context object and table variable
        public GenericRepository(DataContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        #region

        //public IEnumerable<T> GetByName(string nameToFind)
        //{
        //    return table.Find(nameToFind,)
        //}
        #endregion

        #region "Implement interfaces"
        public void Add(T entity)
        {
            table.Add(entity);
        }

        public void Delete(object Id)
        {
            //First, fetch the record from the table
            T existing = table.Find(Id);
            //This will mark the Entity State as Deleted
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            //First attach the object to the table
            table.Attach(entity);
            //Then set the state of the Entity as Modified
            _context.Entry(entity).State = EntityState.Modified;
        }
        #endregion
    }
}
