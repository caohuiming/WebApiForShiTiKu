using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApiForShiTiKu.Entitys;
using WebApiForShiTiKu.Entitys.Result;
using WebApiForShiTiKu.Entitys.ShiTi;

namespace WebApiForShiTiKu.Controllers
{
    [RoutePrefix("api/ShiTi")] 
    public class ShiTiController : ApiController
    {

        [Route("GetEnableShiTiByCondition")]
        [HttpPost]
        public ReturnResultEntity<PageResultEntity<ShiTiEntity>> GetEnableShiTiByCondition(ShiTiSearchEntity shiTiSearchEntity)
        {
            ReturnResultEntity<PageResultEntity<ShiTiEntity>> returnMessageEntity = new ReturnResultEntity<PageResultEntity<ShiTiEntity>>();
            if (shiTiSearchEntity == null)
            {
                returnMessageEntity.success = false;
                returnMessageEntity.errorMsg = "传入参数为null";
                return returnMessageEntity;
            }
            
            List<ShiTiEntity> listShiTi= new List<ShiTiEntity>();
            int nRowCount = 0;
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append(" where is_deleted=0  ");
            if (shiTiSearchEntity.shiTiId > 0)
            {
                sbWhere.Append(string.Format(" and id ={0} ", shiTiSearchEntity.shiTiId));
            }
            else
            {
                if (shiTiSearchEntity.shiJuanId > 0)
                {
                    sbWhere.Append(string.Format(" and shi_juan_id ={0} ", shiTiSearchEntity.shiJuanId));
                }
                if (shiTiSearchEntity.tiXing != null && shiTiSearchEntity.tiXing != "全部")
                {
                    sbWhere.Append(string.Format(" and ti_xing ='{0}' ", shiTiSearchEntity.tiXing));
                }

                if (shiTiSearchEntity.zhengWen != null)
                {
                    sbWhere.Append(string.Format(" and zheng_wen like'%{0}%' ", shiTiSearchEntity.zhengWen));
                }
            }
           
            string strSqlRowCount = "select count(1) rowCount from shi_ti " + sbWhere.ToString();
            object objRowCount = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteScalar(strSqlRowCount);
            if (objRowCount != null)
            {
                nRowCount = Convert.ToInt32(objRowCount);
            }
            string strLimit = string.Format(" order by id limit {0},{1}",
                shiTiSearchEntity.pageIndex * shiTiSearchEntity.pageSize, shiTiSearchEntity.pageSize);
            string strSql = "select *from shi_ti" + sbWhere.ToString() + strLimit;

            DataTable dtShiJuan = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteTable(strSql);
            if (dtShiJuan != null && dtShiJuan.Rows.Count > 0)
            {
                foreach (DataRow dr in dtShiJuan.Rows)
                {
                    ShiTiEntity shiTiEntity = new ShiTiEntity();
                    shiTiEntity = ConvertDataRowToShiTiEntity(dr);
                    listShiTi.Add(shiTiEntity);
                }
            }
            PageResultEntity<ShiTiEntity> pageResultEntity = new PageResultEntity<ShiTiEntity>();
            pageResultEntity.rowCount = nRowCount;
            pageResultEntity.dataList = listShiTi;
            returnMessageEntity.success = true;
            returnMessageEntity.data = pageResultEntity;

            return returnMessageEntity;
        }

        [Route("GetAllTiXing")]
        [HttpPost]
        public ReturnResultEntity<List<TiXingEntity>> GetAllTiXing(ShiTiSearchEntity shiTiSearchEntity)
        {
            ReturnResultEntity<List<TiXingEntity>> returnMessageEntity = new ReturnResultEntity<List<TiXingEntity>>();
            if (shiTiSearchEntity == null)
            {
                returnMessageEntity.success = false;
                returnMessageEntity.errorMsg = "传入参数为null";
                return returnMessageEntity;
            }
            try
            {
                List<TiXingEntity> listTiXing = new List<TiXingEntity>();

                string strSql = string.Format("select distinct ti_xing from shi_ti where shi_juan_id={0}", shiTiSearchEntity.shiJuanId);

                DataTable dtTiXing = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteTable(strSql);
                if (dtTiXing != null && dtTiXing.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTiXing.Rows)
                    {
                        string tiXing = dr["ti_xing"] == DBNull.Value ? string.Empty : dr["ti_xing"].ToString();
                        if (string.IsNullOrEmpty(tiXing))
                        {
                            continue;
                        }
                        TiXingEntity tiXingEntity = new TiXingEntity();
                        tiXingEntity.tiXingName = tiXing;
                        tiXingEntity.tiXingValue = tiXing;
                        listTiXing.Add(tiXingEntity);
                    }
                }
                returnMessageEntity.success = true;
                returnMessageEntity.data = listTiXing;
                return returnMessageEntity;
            }catch(Exception ex)
            {
                returnMessageEntity.success = false;
                returnMessageEntity.errorMsg = "获取题型失败," + ex.Message;
                return returnMessageEntity;
            }
        }

        [Route("DeleteShiTi")]
        [HttpPost]
        public ReturnResultEntity<object> DeleteShiTi(ShiTiSearchEntity shiTiSearchEntity)
        {
            ReturnResultEntity<object> returnResultEntity = new ReturnResultEntity<object>();
            bool bSuccess = false;
            try
            {
                string sql = string.Format("UPDATE shi_ti set is_deleted=1,u_t='{1}' where id={0}", shiTiSearchEntity.shiTiId, DateTime.Now);
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
                returnResultEntity.errorMsg = "删除试题异常," + ex.Message;
                return returnResultEntity;
            }
        }
       
        private ShiTiEntity ConvertDataRowToShiTiEntity(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            ShiTiEntity shiTiEntity = new ShiTiEntity();
            long id = 0;
            int shi_juan_id = 0;
            int shi_yong_deng_ji = 0;
            string nan_du = string.Empty;
            int fen_shu = 0;
            int shi_jian = 0;
            string bian_hao = string.Empty;
            string ti_xing = string.Empty;
            string zheng_wen = string.Empty;
            string xuan_xiang = string.Empty;
            string da_an = string.Empty;
            string zbcsxx = string.Empty;
            string csmsjj = string.Empty;
            string ybcsnr = string.Empty;
            string da_an_jie_xi = string.Empty;
            string ping_fen_biao_zhun = string.Empty;
            string chu_chu = string.Empty;
            string chu_ti_ren = string.Empty;
            string bei_zhu = string.Empty;
            string tiao_mu = string.Empty;
            string fen_ce_ming_cheng = string.Empty;
            string tiao_kuan = string.Empty;
            string zhuan_ye = string.Empty;
            string my_da_an = string.Empty;
            id = dr["id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["id"].ToString());
            shi_juan_id = dr["shi_juan_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shi_juan_id"].ToString());
            shi_yong_deng_ji = dr["shi_yong_deng_ji"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shi_yong_deng_ji"].ToString());
            nan_du = dr["nan_du"] == DBNull.Value ? "" : dr["nan_du"].ToString();
            fen_shu = dr["fen_shu"] == DBNull.Value ? 0 : Convert.ToInt32(dr["fen_shu"].ToString());
            shi_jian = dr["shi_jian"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shi_jian"].ToString());
            bian_hao = dr["bian_hao"] == DBNull.Value ? "" : dr["bian_hao"].ToString();
            ti_xing = dr["ti_xing"] == DBNull.Value ? "" : dr["ti_xing"].ToString();
            zheng_wen = dr["zheng_wen"] == DBNull.Value ? "" : dr["zheng_wen"].ToString();
            xuan_xiang = dr["xuan_xiang"] == DBNull.Value ? "" : dr["xuan_xiang"].ToString();
            da_an = dr["da_an"] == DBNull.Value ? "" : dr["da_an"].ToString();
            zbcsxx = dr["zbcsxx"] == DBNull.Value ? "" : dr["zbcsxx"].ToString();
            csmsjj = dr["csmsjj"] == DBNull.Value ? "" : dr["csmsjj"].ToString();
            ybcsnr = dr["ybcsnr"] == DBNull.Value ? "" : dr["ybcsnr"].ToString();
            da_an_jie_xi = dr["da_an_jie_xi"] == DBNull.Value ? "" : dr["da_an_jie_xi"].ToString();
            ping_fen_biao_zhun = dr["ping_fen_biao_zhun"] == DBNull.Value ? "" : dr["ping_fen_biao_zhun"].ToString();
            chu_chu = dr["chu_chu"] == DBNull.Value ? "" : dr["chu_chu"].ToString();
            chu_ti_ren = dr["chu_ti_ren"] == DBNull.Value ? "" : dr["chu_ti_ren"].ToString();
            bei_zhu = dr["bei_zhu"] == DBNull.Value ? "" : dr["bei_zhu"].ToString();
            tiao_mu = dr["tiao_mu"] == DBNull.Value ? "" : dr["tiao_mu"].ToString();
            fen_ce_ming_cheng = dr["fen_ce_ming_cheng"] == DBNull.Value ? "" : dr["fen_ce_ming_cheng"].ToString();
            tiao_kuan = dr["tiao_kuan"] == DBNull.Value ? "" : dr["tiao_kuan"].ToString();
            zhuan_ye = dr["zhuan_ye"] == DBNull.Value ? "" : dr["zhuan_ye"].ToString();

            shiTiEntity.id = id;
            shiTiEntity.shiJuanId = shi_juan_id;

            shiTiEntity.shiYongDengJi = shi_yong_deng_ji;
            shiTiEntity.nanDu = nan_du;
            shiTiEntity.fenShu = fen_shu;
            shiTiEntity.shiJian = shi_jian;

            shiTiEntity.bianHao = bian_hao;
            shiTiEntity.tiXing = ti_xing;
            shiTiEntity.zhengWen = zheng_wen;
            shiTiEntity.xuanXiang = xuan_xiang;

            shiTiEntity.daAn = da_an;
            shiTiEntity.zbcsxx = zbcsxx;
            shiTiEntity.csmsjj = csmsjj;
            shiTiEntity.ybcsnr = ybcsnr;

            shiTiEntity.daAnJieXi = da_an_jie_xi;
            shiTiEntity.pingFenBiaoZhun = ping_fen_biao_zhun;
            shiTiEntity.chuChu = chu_chu;
            shiTiEntity.chuTiRen = chu_ti_ren;

            shiTiEntity.beiZhu = bei_zhu;
            shiTiEntity.tiaoMu = tiao_mu;
            shiTiEntity.fenCeMingCheng = fen_ce_ming_cheng;
            shiTiEntity.tiaoKuan = tiao_kuan;

            shiTiEntity.zhuanYe = zhuan_ye;

            return shiTiEntity;
        }
    }
}