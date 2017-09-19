using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiForShiTiKu.Entitys;
using WebApiForShiTiKu.Entitys.ShiJuan;
using WebApiForShiTiKu.Entitys.ShiTi;
using WebApiForShiTiKu.Helper;

namespace WebApiForShiTiKu.Controllers
{
    [RoutePrefix("api/ShiJuan")]
    public class ShiJuanController : ApiController
    {
        [Route("GetAllEnableShiJuan")]
        [HttpPost]
        public List<ShiJuanEntity> GetAllEnableShiJuan(ShiJuanSearchEntity shiJuanSearchEntity)
        {
            
            List<ShiJuanEntity> listShiJuan = new List<ShiJuanEntity>();
            string strSql = string.Format(@"select *from shi_juan where is_deleted=0 
order by id limit {0},{1};", 
shiJuanSearchEntity.pageIndex* shiJuanSearchEntity.pageSize, shiJuanSearchEntity.pageSize);
            DataTable dtShiJuan= WebApiForShiTiKu.Helper.MySqlHelper.ExecuteTable(strSql);
            if(dtShiJuan!=null && dtShiJuan.Rows.Count > 0)
            {
                foreach(DataRow dr in dtShiJuan.Rows)
                {
                    int id=dr["id"]==DBNull.Value?0:Convert.ToInt32(dr["id"].ToString());
                    string shiJuanName = dr["shi_juan_name"] == DBNull.Value ? string.Empty : dr["shi_juan_name"].ToString();
                    int isDeleted= dr["is_deleted"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_deleted"].ToString());
                    DateTime dtCt = dr["c_t"]==DBNull.Value?DateTime.Now:Convert.ToDateTime(dr["c_t"].ToString());
                    DateTime dtUt = dr["u_t"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["u_t"].ToString());
                    ShiJuanEntity shiJuanEntity = new ShiJuanEntity();
                    shiJuanEntity.id = id;
                    shiJuanEntity.shiJuanName = shiJuanName;
                    shiJuanEntity.isDeleted = isDeleted;
                    shiJuanEntity.cT = dtCt;
                    shiJuanEntity.uT = dtUt;
                    listShiJuan.Add(shiJuanEntity);
                }
            }

            return listShiJuan;
        }

        [Route("GetEnableShiJuanByCondition")]
        [HttpPost]
        public List<ShiJuanEntity> GetEnableShiJuanByCondition(ShiJuanSearchEntity shiJuanSearchEntity)
        {

            List<ShiJuanEntity> listShiJuan = new List<ShiJuanEntity>();
            int nRowCount = 0;

            string strSqlRowCount = string.Format(@"
select count(1) rowCount from shi_juan 
where is_deleted=0 
and shi_juan_name like'%{0}%' 
and c_t BETWEEN '{1}' and '{2}'; ",
    shiJuanSearchEntity.shiJuanName, shiJuanSearchEntity.beginTime, shiJuanSearchEntity.endTime);
            object objRowCount = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteScalar(strSqlRowCount);
            if(objRowCount != null)
            {
                nRowCount = Convert.ToInt32(objRowCount);
            }
            string strSql = string.Format(@"
select *from shi_juan 
where is_deleted=0 
and shi_juan_name like'%{0}%' 
and c_t BETWEEN '{1}' and '{2}'  
order by id limit {3},{4};", 
                shiJuanSearchEntity.shiJuanName, shiJuanSearchEntity.beginTime, shiJuanSearchEntity.endTime, 
                shiJuanSearchEntity.pageIndex * shiJuanSearchEntity.pageSize, shiJuanSearchEntity.pageSize);
            DataTable dtShiJuan = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteTable(strSql);
            if (dtShiJuan != null && dtShiJuan.Rows.Count > 0)
            {
                nRowCount = dtShiJuan.Rows.Count;
                foreach (DataRow dr in dtShiJuan.Rows)
                {
                    int id = dr["id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id"].ToString());
                    string shiJuanName = dr["shi_juan_name"] == DBNull.Value ? string.Empty : dr["shi_juan_name"].ToString();
                    int isDeleted = dr["is_deleted"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_deleted"].ToString());
                    DateTime dtCt = dr["c_t"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["c_t"].ToString());
                    DateTime dtUt = dr["u_t"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["u_t"].ToString());
                    ShiJuanEntity shiJuanEntity = new ShiJuanEntity();
                    shiJuanEntity.id = id;
                    shiJuanEntity.shiJuanName = shiJuanName;
                    shiJuanEntity.isDeleted = isDeleted;
                    shiJuanEntity.cT = dtCt;
                    shiJuanEntity.uT = dtUt;
                    listShiJuan.Add(shiJuanEntity);
                }
            }

            return listShiJuan;
        }

        [Route("AddShiJuanAndShiTis")]
        [HttpPost]
        public bool AddShiJuanAndShiTis(ShiJuanEntity shiJuanEntity)
        {
            try
            {
                if (shiJuanEntity == null)
                {
                    return false;
                }
                if (shiJuanEntity.shiTis == null || shiJuanEntity.shiTis.Count == 0)
                {
                    return false;
                }
                bool bAddShiJuan = AddShiJuan(shiJuanEntity);
                string strSqlSelectMaxId = "select max(id) from shi_juan";
                int nMaxId = 0;
                object objMaxId=  WebApiForShiTiKu.Helper.MySqlHelper.ExecuteScalar(strSqlSelectMaxId);
                if (objMaxId != null)
                {
                    nMaxId = Convert.ToInt32(objMaxId);
                }

                foreach (ShiTiEntity shiTi in shiJuanEntity.shiTis)
                {
                    string sql = String.Format(@"insert into shi_ti 
values(null,{0},{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',
'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',{24})",
                            nMaxId, shiTi.shiYongDengJi, shiTi.nanDu, shiTi.fenShu,
                            shiTi.shiJian, shiTi.bianHao, shiTi.tiXing, shiTi.zhengWen,
                            shiTi.xuanXiang, shiTi.daAn, shiTi.zbcsxx, shiTi.csmsjj,
                            shiTi.ybcsnr, shiTi.daAnJieXi, shiTi.pingFenBiaoZhun, shiTi.chuChu,
                            shiTi.chuTiRen, shiTi.beiZhu, shiTi.tiaoMu, shiTi.fenCeMingCheng,
                            shiTi.tiaoKuan, shiTi.zhuanYe, DateTime.Now, DateTime.Now, 0);
                    int rowNum = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteNonQuery(sql);

                }
                return true;
            }catch
            {
                return false;
            }
        }
        [Route("DeleteShiJuanAndShiTis")]
        [HttpPost]
        public bool DeleteShiJuanAndShiTis(ShiJuanEntity shiJuanEntity)
        {
            try
            {
                string sql = string.Format("UPDATE shi_juan set is_deleted=1 where id={0}", shiJuanEntity.id);
                int rowNum = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteNonQuery(sql);
                if (rowNum > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }      
            }
            catch 
            {
                return false;
            }
        }
        private bool AddShiJuan(ShiJuanEntity shiJuanEntity)
        {
            string sql = string.Format("insert into shi_juan values(null,'{0}',0,'{1}','{2}')",
                shiJuanEntity.shiJuanName,shiJuanEntity.cT,shiJuanEntity.uT);
            int nNum= WebApiForShiTiKu.Helper.MySqlHelper.ExecuteNonQuery(sql);
            if (nNum > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}