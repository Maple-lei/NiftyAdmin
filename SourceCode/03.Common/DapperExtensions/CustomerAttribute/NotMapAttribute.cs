using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtensions.CustomerAttribute
{
    /// <summary>
    /// 不与数据库进行匹配的字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class NotMapAttribute : Attribute
    {
        public NotMapAttribute()
        {
        }
    }
}
