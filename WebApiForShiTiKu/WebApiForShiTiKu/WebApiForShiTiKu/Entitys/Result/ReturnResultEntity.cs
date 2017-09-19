using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiForShiTiKu.Entitys.Result
{
    public class ReturnResultEntity<T> where T:class
    {
        public bool success { get; set; }//是否成功
        public T data;//返回数据
        public string errorMsg { get; set; }//错误信息
    }
}