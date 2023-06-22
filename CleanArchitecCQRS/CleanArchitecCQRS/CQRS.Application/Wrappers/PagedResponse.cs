using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        public PagedResponse(T data)
        {
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        /// <summary>
        /// Paged response with count all iteam
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalItem"></param>
        public PagedResponse(T data, int pageNumber, int pageSize, int totalItems)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalItems = totalItems;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
