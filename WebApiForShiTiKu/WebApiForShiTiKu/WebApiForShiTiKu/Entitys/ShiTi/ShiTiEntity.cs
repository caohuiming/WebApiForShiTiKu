using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiForShiTiKu.Entitys.ShiTi
{
    public class ShiTiEntity: PageSearchEntity
    {
        public long id { get; set; }
        public int shiJuanId { get; set; }//试卷编号
        public int shiYongDengJi{ get; set; }//适用等级
        public string nanDu{ get; set; }//难度
        public int fenShu{ get; set; }//答题分数
        public int shiJian{ get; set; }//答题时间
        public string bianHao{ get; set; }//试题编号
        public string tiXing{ get; set; }//题型
        public string zhengWen{ get; set; }//正文
        public string xuanXiang{ get; set; }//试题选项
        public string daAn{ get; set; }//试题答案
        public string zbcsxx{ get; set; }//自变参数选项
        public string csmsjj{ get; set; }//参数M数据集
        public string ybcsnr{ get; set; }//应变参数内容
        public string daAnJieXi{ get; set; }//答案解析
        public string pingFenBiaoZhun { get; set; }//答案要点及评分标准
        public string chuChu{ get; set; }//依据 出处
        public string chuTiRen{ get; set; }//出题人
        public string beiZhu{ get; set; }//备注
        public string tiaoMu{ get; set; }//条目
        public string fenCeMingCheng{ get; set; }//分册名称
        public string tiaoKuan{ get; set; }//条款
        public string zhuanYe{ get; set; }//专业

        public DateTime cT { get; set; }
        public DateTime uT { get; set; }
        public int isDeleted { get; set; }
    }
}
