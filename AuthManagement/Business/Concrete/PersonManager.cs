using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.URI;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PersonManager : IPersonService
    {
        //bunalrındependency  sşnş autofacte yaptığından emin ol
        private readonly IPersonDal _personDal;
        protected readonly IMapper _mapper;
        private readonly IUriService _uriService;

        

        public PersonManager(IPersonDal personDal, IUriService uriService, IMapper mapper)
        {
            _personDal = personDal;
            _mapper = mapper;
            _uriService = uriService;
        }

      

        

        public DataResult<Person> GetByPersonId(int personId)
        {
            return new SuccessDataResult<Person>(_personDal.Get(C => C.PersonId== personId));
        }
        public Result Add(Person person)//bunlar account id yi nasıl otomatik çekecek
        {
            _personDal.Add(person);
            return new SuccessDataResult<Person>(Messages.PersonAdded);
        }

        public Result Delete(Person person)
        {
            _personDal.Delete(person);
            return new Result(true, Messages.PersonDeleted);
        }
        public Result Update(Person person)
        {
            _personDal.Update(person);//I resultta result oluyor-update de mesaj veya eri gelimyo sadece bool
            return new Result(true);
        }

        public async Task<IDataResult<IEnumerable<PersonDto>>> GetPaginationAsync(PaginationFilter paginationFilter, PersonDto filterResource,string route)
        {
            
            /*if (true)
            {
                //cachede var ise
            }
            else
            {
               //cachede yok ise
            }*/

            var paginationPerson = await _personDal.GetPaginationAsync(paginationFilter, filterResource);
            
            var tempRecords = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(paginationPerson.records);

            PaginatedResult<IEnumerable<PersonDto>> resource = PaginationHelper.CreatePaginatedResponse(tempRecords, paginationFilter, paginationPerson.total, _uriService, route);
            
            return resource;

        }
         
       
    }
}
