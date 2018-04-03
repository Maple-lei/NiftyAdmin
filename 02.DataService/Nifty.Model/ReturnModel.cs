using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nifty.Model
{
    /// <summary>
    /// 数据返回对象，通常在调用接口返回时用到
    /// </summary>
    public class ReturnModel
    {
        public int Code { get; set; }

        public object Data { get; set; }

        public object Message { get; set; }
    }
}
