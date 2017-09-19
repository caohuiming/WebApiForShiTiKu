using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiForShiTiKu.Entitys;
using WebApiForShiTiKu.Entitys.Result;
using WebApiForShiTiKu.Entitys.User;
using WebApiForShiTiKu.Helper;

namespace WebApiForShiTiKu.Controllers
{
    [RoutePrefix("api/UserInfo")]
    public class UserInfoController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public ReturnResultEntity<MyUserEntity> Login( MyUserEntity myUserPar)
        {
            ReturnResultEntity<MyUserEntity> returnResultEntity = new ReturnResultEntity<MyUserEntity>();
            try
            {
                if (myUserPar == null)
                {
                    returnResultEntity.success = false;
                    returnResultEntity.errorMsg = "参数不能为空";
                    return returnResultEntity;
                }
                MyUserEntity myUserRtn = null;
                string sql = string.Format("SELECT * from my_user where user_name='{0}' and user_pwd='{1}' and is_deleted=0",
                    myUserPar.userName, myUserPar.userPwd);
                DataTable dtUser = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteTable(sql);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    DataRow dr = dtUser.Rows[0];
                    myUserRtn = new MyUserEntity();
                    myUserRtn.userId = dr["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["user_id"].ToString());
                    myUserRtn.userName = dr["user_name"] == DBNull.Value ? string.Empty : dr["user_name"].ToString();
                    myUserRtn.userPhone = dr["user_phone"] == DBNull.Value ? string.Empty : dr["user_phone"].ToString();
                    myUserRtn.userPwd = dr["user_pwd"] == DBNull.Value ? string.Empty : dr["user_pwd"].ToString();
                    myUserRtn.cT = dr["c_t"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["c_t"].ToString());
                    myUserRtn.uT = dr["u_t"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["u_t"].ToString());
                    myUserRtn.isDeleted = dr["is_deleted"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_deleted"].ToString());
                }
                returnResultEntity.success = true;
                returnResultEntity.data = myUserRtn;
                return returnResultEntity;
            }catch(Exception ex)
            {
                returnResultEntity.success = false;
                returnResultEntity.errorMsg = "登录异常," + ex.Message;
                return returnResultEntity;
            }
        }
        [Route("UpdateUser")]
        [HttpPost]
        public ReturnResultEntity<object> UpdateUser(MyUserEntity myUserPar)
        {
            ReturnResultEntity<object> returnResultEntity = new ReturnResultEntity<object>();
            bool bSuccess = false;
            try
            {
                string sqlOldPwd = string.Format("SELECT user_pwd from my_user where user_id={0} and is_deleted=0 limit 1",
               myUserPar.userId);
                object objOldPwd = WebApiForShiTiKu.Helper.MySqlHelper.ExecuteScalar(sqlOldPwd);
                if (myUserPar.userPwd != objOldPwd.ToString())
                {
                    returnResultEntity.success = false;
                    returnResultEntity.errorMsg = "旧密码输入错误";
                    return returnResultEntity;
                }
                string sql = string.Format("UPDATE my_user set user_pwd='{1}',u_t='{2}' where user_id={0}", myUserPar.userId, myUserPar.userPwdNew, DateTime.Now);
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
                returnResultEntity.errorMsg = "修改密码异常," + ex.Message;
                return returnResultEntity;
            }
        }

    }
}