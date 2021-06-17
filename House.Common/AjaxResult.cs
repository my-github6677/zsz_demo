using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Common
{
    public class AjaxResult
    {
        /// <summary>
        /// 执行的结果
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 执行的返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
