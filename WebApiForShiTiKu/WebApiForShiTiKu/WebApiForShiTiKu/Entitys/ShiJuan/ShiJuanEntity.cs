using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiForShiTiKu.Entitys.ShiTi;

namespace WebApiForShiTiKu.Entitys.ShiJuan
{
    public class ShiJuanEntity
    {
        public int id { get; set; }
        public string shiJuanName { get; set; }
        public int isDeleted { get; set; }
        public DateTime cT { get; set; }
        public DateTime uT { get; set; }

        public List<ShiTiEntity> shiTis { get; set; }
    }
}