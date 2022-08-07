using Core.Entities.Concrete;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.URI
{
    public class UriManager : IUriService
    {
        /// <summary>
        /// Application url in launchSettings.json
        /// </summary>
        private readonly string _baseUri;

        public UriManager(string baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// Get page uri from request
        /// </summary>
        /// <param name="filter">Pagination filter; page size, page number</param>
        /// <param name="route">API endpoint without base uri</param>
        /// <returns>Request URI with pagination</returns>
        public Uri GeneratePageRequestUri(PaginationFilter filter, string route)
        {
            var endpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri =
                QueryHelpers.AddQueryString(endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
