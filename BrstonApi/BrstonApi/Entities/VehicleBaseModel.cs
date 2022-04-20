using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities
{
    /// <summary>
    /// 该模型弃用 
    /// 改用调用存储过程，返回结果到SearchVINView
    /// </summary>
    [Serializable]
    [Table("T_DMS")]
    public class VehicleBaseModel
    {
        //[Column("样式(SFX)")]
        //[Column(TypeName = "BIT")]
        //public string 样式 { get; set; }

        [Key]
        public int ID { get; set; }
        public string 车辆识别代号 { get; set; }
        public string 车辆识别代号MD5 { get; set; }
        public DateTime? 上传日期 { get; set; }
        public DateTime? 车辆制造日期 { get; set; }
        public string 车辆型号 { get; set; }
        public string 车辆名称 { get; set; }
        public string 企业集团 { get; set; }
        public string 外资集团 { get; set; }
        public string 申报企业名称 { get; set; }
        public string 申报企业类型 { get; set; }
        public string 车辆生产单位名称 { get; set; }
        public string 制造商全称 { get; set; }
        public string 车型编号 { get; set; }
        public string 制造商 { get; set; }
        public string 制造商英文 { get; set; }
        public string 品牌 { get; set; }
        public string 品牌英文 { get; set; }
        public string 车辆品牌 { get; set; }
        public string 车系 { get; set; }
        public string 车系英文 { get; set; }
        public string 车型 { get; set; }
        public string 车型英文 { get; set; }
        public string 款式编号 { get; set; }
        public string 年款 { get; set; }
        public string 销售版本 { get; set; }
        public string 厂商指导价 { get; set; }
        public string 国别 { get; set; }
        public string 车辆类型 { get; set; }
        public string 车型分类 { get; set; }
        public string 车身形式 { get; set; }
        public string 车型细分 { get; set; }
        public string 生产方式 { get; set; }
        public string 最高车速 { get; set; }
        public string 油耗 { get; set; }
        public string 车身颜色 { get; set; }
        public string 外廓长 { get; set; }
        public string 外廓宽 { get; set; }
        public string 外廓高 { get; set; }
        public string 轴距 { get; set; }
        public string 前轮距 { get; set; }
        public string 后轮距 { get; set; }
        public string 轴荷 { get; set; }
        public string 轴数 { get; set; }
        public string 钢板弹簧片数 { get; set; }
        public string 整备质量 { get; set; }
        public string 总质量 { get; set; }
        public string 额定载客 { get; set; }
        public string 驾驶室准乘人数 { get; set; }
        public string 发动机型号 { get; set; }
        public string 发动机号 { get; set; }
        public string 发动机企业 { get; set; }
        public string 气缸容积 { get; set; }
        public string 排量 { get; set; }
        public string 进气形式 { get; set; }
        public string 功率 { get; set; }
        public string 燃料种类 { get; set; }
        public string 排放标准 { get; set; }
        public string 变速箱 { get; set; }
        public string 挡位个数 { get; set; }
        public string 变速箱类型 { get; set; }
        public string 转向形式 { get; set; }
        public string 底盘型号 { get; set; }
        public string 底盘品牌 { get; set; }
        public string 底盘生产企业 { get; set; }
        public string 转向轴数 { get; set; }
        public string 轮胎规格 { get; set; }
        public string 轮胎数 { get; set; }
        public string 货厢内部长 { get; set; }
        public string 货厢内部宽 { get; set; }
        public string 货厢内部高 { get; set; }
        public string 额定载质量 { get; set; }
        public string 载质量利用系数 { get; set; }
        public string 准牵引总质量 { get; set; }
        public string 半挂车鞍座最大允许总质量 { get; set; }
        public string 车辆分类 { get; set; }
        public string 车辆分类1 { get; set; }
        public string 车辆分类2 { get; set; }
        public string 车辆分类3 { get; set; }
        public string 车辆分类4 { get; set; }
    }
}
