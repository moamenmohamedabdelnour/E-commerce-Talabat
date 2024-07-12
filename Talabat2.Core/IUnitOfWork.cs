using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Repositories;

namespace Talabat2.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        //Will Use Generic Method Return GenericRepository When Need To Return Any Repository Which Match BaseEntity
        IGenericRepository<TEntity>Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
