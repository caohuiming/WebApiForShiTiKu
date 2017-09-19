using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiForShiTiKu.Entitys.Result
{
    public class PageResultEntity<T> where T : class
    {
        public int rowCount { get; set; }//总行数
        public List<T> dataList;//数据列表
    }
}