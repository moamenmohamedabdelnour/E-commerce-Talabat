using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Specification;

namespace Talabat2.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> criteria { get ; set ; }//Automatic Prop For Where 
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();//Automatic Prop For Include
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPaginationEnabled { get ; set ; }

        //To Use This Expression Must Intialize It Before When Controller Call This Class
        //We Will Intialize It In Constructor 

        //If Controller Need To Create Includes Will Create Empty Constructor 
        public BaseSpecification()
        {
            // Without Where Condition
            //Only Crrate Includes
        }
        //If Will Use Where Condition Will Create Constructor Take 
        public BaseSpecification(Expression<Func<T, bool>> criteria)//Waiting Where Expression (Like P=>P.Id==Id)
        {
            //Intalize Where
            this.criteria=criteria; 
            //Include Intialize Above with Any Calling To This Class With Any Constructor
        }

        //Create 2 Fuction To Create Expression For OrderBy And OrderByDescending 
        public void AddOrderBy (Expression<Func<T, object>> OrderBy)
        { 
            this.OrderBy=OrderBy;
        }
        public void AddOrderByDescendeing(Expression<Func<T, object>> OrderByDescending)
        {
            this.OrderByDescending=OrderByDescending;
        }

        public void ApplyPagination (int skip ,int take)
        {
            IsPaginationEnabled = true;
            this.Skip=skip;
            this.Take=take;
        }
        //By This Class I Have Expression Of Linq Operator 
        //THEN I NEED TO CREATE DYNAMIC QUERY
    }
}
