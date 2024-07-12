using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;

namespace Talabat2.Core.Specification
{
    //Create This Interface To create Signatures of Lambda Expression To Use It In Any EF
    public interface ISpecification<T> where T : BaseEntity
    {
        //To Create Where Lambda Expression We Know Where (Take Object <T> And Return Bool Of True or False)
        public Expression<Func<T, bool>> criteria { get; set; }// Signature For Prop

        // To Create Lambda For Include It May be More Than One So We Must Make It List 
        public List<Expression<Func<T,object>>>Includes { get; set; }//Signature For Prop

        //To Create Lambda Expression For OrderBy which Take Generic<T> And Return Object
        public Expression<Func<T,object>> OrderBy { get; set; }

        //To Create Lambda Expression For OrderByDescending which Take Generic<T> And Return Object
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        //To Use Pagination Will Use Skip & Take Linq Operator
        public int Skip { set; get; }
        public int Take { set; get; }
        public bool IsPaginationEnabled { set; get; }

        //Any Another Expression Will Assign It's Signature Here To 40 Linq Operator
    }
}
