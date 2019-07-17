using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ServicesClass
{
    public class Common:ICommon
    {
        public Common() { }
        public Common(bool param)
        {

        }
        public int count(int param)
        {
            return param;
        }
    }
}