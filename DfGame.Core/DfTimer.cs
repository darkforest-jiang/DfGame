using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class DfTimer
    {
        /// <summary>
        /// 增量时间/s
        /// </summary>
        public static float DeltaTime = 0;
        /// <summary>
        /// 上次执行时间/ticks  1毫秒=10000ticks
        /// </summary>
        public static long LastTicks = 0;
    }
}
