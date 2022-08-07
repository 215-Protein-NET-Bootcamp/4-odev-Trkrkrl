using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.URI
{
    public interface IUriService
    {
        System.Uri GeneratePageRequestUri(PaginationFilter filter, string route);
    }
}
