using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Specification;

namespace Talabat2.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //Only Signature Of Methods Will Use In Any Context 
        //Methods Work Static 
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        //Methods Work Dynamic 
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);

        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

        Task Add (T entity);
        void Update (T entity);
        void Delete (T entity);
    }
}
