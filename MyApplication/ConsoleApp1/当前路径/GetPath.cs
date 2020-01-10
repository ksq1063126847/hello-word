using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.当前路径
{
    public class GetPath
    {
        public string Get()
        {
            string result = "";
            //System.Web.HttpContext.Current.Server.MapPath("~/");
            //System.Windows.Forms.Application.StartupPath;

            //完全处理，假设出现权限问题导致无法备份，备份到数据库安装目录
            //result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\Mainsoft\";

            //EF备份数据库示例 ：context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "backup database " + dbname + " to disk='" + outfilename + "'");
            return result;
        }
    }
}
