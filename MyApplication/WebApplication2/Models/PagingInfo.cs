using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }//总条数
        public int ItemsPerPage { get; set; }//每页条数
        public int CurrentPage { get; set; }//当前页
        public int TotalPages //总页数
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}