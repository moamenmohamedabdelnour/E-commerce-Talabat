using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core;
using Talabat2.Core.Entites;
using Talabat2.Core.Repositories;
using Talabat2.Repository.Data;

namespace Talabat2.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext2 context;
        //Will Use HashTable To Save Unique Repo And Return It If Needed In Same Request And Using It To Add More One Repo Which Using In One Request
        private Hashtable _repositories;
        public UnitOfWork(StoreContext2 context)
        {
            this.context=context;
            _repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                //By This Function Will Create GenericRepo When And Which I Need
                var repository = new GenericRepository<TEntity>(context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
        public async Task<int> Complete()
        => await context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await context.DisposeAsync();
    }
}
