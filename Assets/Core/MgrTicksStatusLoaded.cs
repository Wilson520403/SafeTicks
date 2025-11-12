using System;
using UnityEngine;

namespace SafeTicks
{
    /// <summary>
    /// 已初始化时间戳
    /// </summary>
    public class MgrTicksStatusLoaded : MgrTicksStatus
    {
        /// <summary>
        /// 可以接受的最大时间误差，60 秒，否则重新同步时间
        /// </summary>
        static long ABS_MAX = 10 * 1000 * 10000;

        /// <summary>
        /// 进入该状态时候的时间戳
        /// </summary>
        long _ticksStart;

        /// <summary>
        /// 最后一次获取到的时间戳
        /// </summary>
        long _ticksCurrent;

        public override void OnEnter ()
        {
            _ticksStart = GetTicks ();
            _ticksCurrent = _ticksStart;
        }

        public override void OnUpdate ()
        {
            long ticks = GetTicks ();

            // 与上一次的时间戳差值过大
            if (ABS_MAX < Math.Abs (ticks - _ticksStart))
            {
                src.Enter (src.statusLoading);
                return;
            }

            // 否则认为没问题，对外公开该时间戳
            _ticksCurrent = ticks;

            // 对外派发事件
            src.safeTimestamp.OnUpdate (_ticksCurrent);
        }

        long GetTicks ()
        {
            return src.ticksServerSubLocal + DateTime.Now.Ticks;
        }
    }
}