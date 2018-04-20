using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiftyDapper
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    public class PageModel
    {
        //表或视图名称
        public string TableName { get; set; }

        //要输出的字段
        public string Fields { get; set; }

        //where过滤(传进来的参数中不带where字符)
        public string Where { get; set; }

        //排序字段(传进来的参数不带order by 字符)
        public string OrderBy { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// 查询数据的总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
