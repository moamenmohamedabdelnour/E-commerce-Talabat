using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Specification;

namespace Talabat2.Repository
{
    //This Class To Create Query But Dynamic
    //Must Be Static Becouse Have Static Function Will Called To Create Query To Specification <T>
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        //Query = Base [Context.Set<T>().] + Logic (Linq Operators)
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity>inputQuery,ISpecification<TEntity> spec)
        {
            // first Add base To Query To Know Any Table In Any DataBase Will Work
            //query=dbContext.Products
            var query = inputQuery;
            //Check criteria becouse It Optianal
            if (spec.criteria is not null)
                query =query.Where(spec.criteria);//query=dbContext.Products.where(P=>P.Id==id)

            //add OrderBy And OrderByDesc To Query If Arenot null
            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if(spec.OrderByDescending is not null) 
                query = query.OrderByDescending(spec.OrderByDescending);

            if(spec.IsPaginationEnabled)
                query=query.Skip(spec.Skip).Take(spec.Take);
            //Adding Include To Query Becouse It More Than One Will Use Aggregate Function To Add All Include Expression To Query
            query=spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            //Put query In CurrentQuery And Put Include Expression From Spec.Icludes In IncludeExpression Then Include IncludeExpression In CurrentQuery
            //Do It For all Include Expression In Spec By Put It In IncludeExpression Every Time And Include It To query

            //query=dbContext.Products.where(P=>P.Id==id).Include(P=>P.ProductBrand).Include(P=>P.ProductType);

            return query;

            //Who Will Use This Query ???
            //-- Methods In Generic Repository So Go TO Generic Repo To Create Methods Use Dynamic Query

        }
    }
}
