using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{   
    public class TestEntity
    {
        [Required(ErrorMessage ="姓名不能为空")]
        public string Name { get; set; }
        public string PhoneNum { get; set; }
    }
}