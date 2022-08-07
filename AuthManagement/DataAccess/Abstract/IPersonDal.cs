using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPersonDal: IBaseRepository<Person>
    {
       
        Task <(IEnumerable<Person> records, int total)>GetPaginationAsync(PaginationFilter paginationFilter, PersonDto filterResource);
    }
}
