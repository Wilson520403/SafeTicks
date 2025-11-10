namespace SafeTicks
{
    /// <summary>
    /// 时间正常流动
    /// </summary>
    public class SafeTimestampStatusOrdinary : SafeTimestampStatus
    {
        public override void OnUpdateAllOverTheWorld ()
        {
            // 理想情况，时间递增
            if (rel.TicksSafe <= rel.ticksSysEnv)
            {
                rel.TicksSafe = rel.ticksSysEnv;
            }

            // 时光倒流了
            else
            {
                rel.Enter (rel.statusReverse);
            }
        }
    }
}