namespace SafeTicks
{
    /// <summary>
    /// 针对时间倒流的状态处理
    /// 采用一种“相对增量”的平滑恢复策略。
    /// 尽管绝对时间点不可信，但时间流逝的相对间隔是可信的。从而让安全时间基于自己之前的基准继续前进，忽略系统时间的绝对值跳变。
    /// </summary>
    public class SafeTimestampStatusReverse : SafeTimestampStatus
    {
        /// <summary>
        /// 起始时间戳
        /// </summary>
        public long ticksStart;

        /// <summary>
        /// 时间步长参考的时间
        /// </summary>
        public long ticksAnchor;

        public override void OnEnter ()
        {
            ticksStart = rel.TicksSafe;
            ticksAnchor = rel.ticksSysEnv;
        }

        public override void OnUpdateAllOverTheWorld ()
        {
            // 时间戳变合理了，回到正常模式
            if (rel.TicksSafe <= rel.ticksSysEnv)
            {
                rel.TicksSafe = rel.ticksSysEnv;
                rel.Enter (rel.statusOrdinary);
                return;
            }

            //现实时间流逝的增量
            var addVal = rel.ticksSysEnv - ticksAnchor;

            //根据增量获取的新时间
            long ticksSafe = ticksStart + addVal;

            if (rel.TicksSafe <= ticksSafe)
            {
                rel.TicksSafe = ticksSafe;
                return;
            }

            //时间倒流的情况为解决，重新计算增量
            rel.Enter (rel.statusReverse);
        }
    }
}