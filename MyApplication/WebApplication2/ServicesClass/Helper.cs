using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ServicesClass
{
    public class Helper:IHelper
    {
        ICommon common;
        public Helper(ICommon common)
        {
            this.common = common;
        }

        public string GetYear()
        {
            return DateTime.Now.Year.ToString();
        }
    }
}