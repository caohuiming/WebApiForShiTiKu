using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiForShiTiKu.Entitys.ShiTi
{
    public  class ShiTiSearchEntity:PageSearchEntity
    {
        public int shiJuanId { get; set; }
        public int shiTiId { get; set; }
        public string tiXing { get; set; }
        public string zhengWen { get; set; }
    }
}
