using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/**
 * model of global result
 * @author WUYUDING
 */
namespace SSIS_FRONT.Components
{
    public class Result
    {
        public int code { get; set; } = 200;

        public object data { get; set; }

        public string msg { get; set; }

        public string sub_msg { get; set; }
    }
}
