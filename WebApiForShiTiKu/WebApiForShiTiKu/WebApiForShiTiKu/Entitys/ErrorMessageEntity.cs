using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiForShiTiKu.Entitys
{
    public class ErrorMessageEntity
    {
        public static string ExcuteSqlException = "101|执行数据库异常";
        public static string UnknownException = "500|未知异常";
    }
}