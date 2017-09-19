using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiForShiTiKu.Entitys
{
    public class PageSearchEntity
    {
        public int pageIndex { get; set; }//从0开始
        public int pageSize { get; set; }
    }
}
