using System;

namespace SafeTicks
{
    /// <summary>
    /// 正在加载时间戳
    /// </summary>
    public class MgrTicksStatusLoading : MgrTicksStatus
    {
        public override void OnEnter ()
        {
            FixTicks ();
        }

        public async void FixTicks ()
        {
            long ticks = MgrTicks.ParseTimestampMillisecondToTicks (await src.env.GetTimestampMillisecond ());
            src.ticksServerSubLocal = ticks - DateTime.Now.Ticks;
            src.Enter (src.statusLoaded);
        }
    }
}