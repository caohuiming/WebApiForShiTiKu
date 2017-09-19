using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApiForShiTiKu.Entitys;
using WebApiForShiTiKu.Entitys.Result;
using WebApiForShiTiKu.Entitys.ShiJuan;

namespace WebApiForShiTiKu.Controllers
{
    [RoutePrefix("api/ShiJuanNew")]
    public class ShiJuanNewController : ApiController
    {
        [Route("GetEnableShiJuanByCondition")]
        [HttpPost]
        public ReturnResultEntity<PageResultEntity<ShiJuanEntity>> GetEnableShiJuanByCondition(ShiJuanSearchEntity shiJuanSearchEntity)
        {
            ReturnResultEntity<PageResultEntity<ShiJuanEntity>> returnMessageEntity = new ReturnResultEntity<PageResultEntity<ShiJuanEntity>>();
            List<ShiJuanEntity> listShiJuan = new List<ShiJuanEntity>();
            int nRowCount = 0;
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append(" where is_deleted=0  ");
            if (shiJuanSearchEntity.shiJuanName != null)
            {
                sbWhere.Append(string.Format(" and shi_juan_name like'%{0}%' ", shiJuanSearchEntity.shiJuanName));
            }
            if (shiJuanSearchEntity.beginTime != null)
            {
                sbWhere.Append(string.Format(" and c_t >='{0}' ", shiJuanSearchEntity.beginTime));
            }
            if (shiJuanSearchEntity.endTime != null)
            {
                sbWhere.Append(string.Format(" and c_t <='{0}' ", shiJuanSearchEntity.endTime));
            }
            string strSqlRowCount = "select count(1) rowCount from shi_juan " + sbWhere.ToString();
            object objRowCount = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteScalar(strSqlRowCount);
            if (objRowCount != null)
            {
                nRowCount = Convert.ToInt32(objRowCount);
            }
            string strLimit = string.Format(" order by id limit {0},{1}",
                shiJuanSearchEntity.pageIndex* shiJuanSearchEntity.pageSize, shiJuanSearchEntity.pageSize);
            string strSql = "select *from shi_juan" + sbWhere.ToString()+strLimit;

            DataTable dtShiJuan = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteTable(strSql);
            if (dtShiJuan != null && dtShiJuan.Rows.Count > 0)
            {
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
            PageResultEntity<ShiJuanEntity> pageResultEntity = new PageResultEntity<ShiJuanEntity>();
            pageResultEntity.rowCount = nRowCount;
            pageResultEntity.dataList = listShiJuan;
            returnMessageEntity.success = true;
            returnMessageEntity.data = pageResultEntity;

            return returnMessageEntity;
        }

        [Route("DeleteShiJuan")]
        [HttpPost]
        public ReturnResultEntity<object> DeleteShiJuan(ShiJuanEntity shiJuanEntity)
        {
            ReturnResultEntity<object> returnResultEntity = new ReturnResultEntity<object>();
            bool bSuccess = false;
            try
            {
                string sql = string.Format("UPDATE shi_juan set is_deleted=1,u_t='{1}' where id={0}", shiJuanEntity.id, DateTime.Now);
                int rowNum = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteNonQuery(sql);
                if (rowNum > 0)
                {
                    bSuccess= true;
                }
                returnResultEntity.success = bSuccess;
                returnResultEntity.data = bSuccess;
                return returnResultEntity;
            }
            catch (Exception ex)
            {
                returnResultEntity.success = false;
                returnResultEntity.errorMsg = "删除试卷异常," + ex.Message;
                return returnResultEntity;
            }
        }

        [Route("UpdateShiJuan")]
        [HttpPost]
        public ReturnResultEntity<object> UpdateShiJuan(ShiJuanEntity shiJuanEntity)
        {
            ReturnResultEntity<object> returnResultEntity = new ReturnResultEntity<object>();
            bool bSuccess = false;
            try
            {
                string sql = string.Format("UPDATE shi_juan set shi_juan_name='{1}',u_t='{2}' where id={0}", shiJuanEntity.id, shiJuanEntity.shiJuanName, DateTime.Now);
                int rowNum = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteNonQuery(sql);
                if (rowNum > 0)
                {
                    bSuccess = true;
                }
                returnResultEntity.success = bSuccess;
                returnResultEntity.data = bSuccess;
                return returnResultEntity;
            }
            catch (Exception ex)
            {
                returnResultEntity.success = false;
                returnResultEntity.errorMsg = "删除试卷异常," + ex.Message;
                return returnResultEntity;
            }
        }
    }
}