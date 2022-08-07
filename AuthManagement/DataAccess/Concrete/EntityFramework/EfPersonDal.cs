using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPersonDal: EfEntityBaseRepository<Person, EfCoreDbContext>, IPersonDal
    {
    
        public EfPersonDal(EfCoreDbContext context):base(context)   
        {
            //bunun düzgün çalışması için efrepobasedeki ctoru düzenle
        }

        public async Task<(IEnumerable<Person> records, int total)> GetPaginationAsync(PaginationFilter paginationFilter, PersonDto filterResource)
        {
            
                var queryable = ConditionFilter(filterResource);

                var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
            .AsSplitQuery()
            .OrderBy(x => x.PersonId)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

                return (records, total);
            


        }
        

        private IQueryable<Person> ConditionFilter(PersonDto personDto)
        {
            
                var queryable = Context.Persons.AsQueryable();

                if (personDto != null)
                {
                    if (!string.IsNullOrEmpty(personDto.PersonId))
                        queryable = queryable.Where(x => x.PersonId.Equals(personDto.PersonId));

                    if (!string.IsNullOrEmpty(personDto.FirstName))
                    {
                        string fullName = personDto.FirstName.ToLower();
                        queryable = queryable.Where(x => x.FirstName.Contains(fullName));
                    }

                    if (!string.IsNullOrEmpty(personDto.LastName))
                    {
                        string fullName = personDto.LastName.ToLower();
                        queryable = queryable.Where(x => x.LastName.Contains(fullName));
                    }
                }

                return queryable;
            
                
        }

    }
}
