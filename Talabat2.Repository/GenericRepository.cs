using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Repositories;
using Talabat2.Core.Specification;
using Talabat2.Repository.Data;

namespace Talabat2.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext2 dbContext;
        //Constructor To Use Specific DataBase 
        //Each Repo Can Call Other DataBase

        public GenericRepository(StoreContext2 dbContext) 
        {

            this.dbContext=dbContext;
        }
        #region Static Query
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //will Comment that becouse will use Specification Pattern To Create Dynamic Query
            if (typeof(T)==typeof(Product))
                return (IReadOnlyList<T>)await dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {


            return await dbContext.Set<T>().FindAsync(id);
        } 
        #endregion
        //if call this Methods In Controller Should Path Specific Specififcation 
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(); 

        }
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        //To Have Query Using It In This Methods We Must Call SpecificationEvalutor By Next Function
        private IQueryable<T> ApplySpecification(ISpecification<T> spec) 
        {
           return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(), spec);   
        }

        public async Task Add(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
    }
}
