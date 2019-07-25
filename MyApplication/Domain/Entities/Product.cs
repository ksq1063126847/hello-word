using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "请输入产品名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入产品描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "请输入产品价格")]
        [Range(0.01, double.MaxValue, ErrorMessage = "请输入有效的价格")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "请指定类别")]
        public string Category { get; set; }
    }
}
