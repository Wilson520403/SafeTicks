namespace SafeTicks
{
    /// <summary>
    /// 已初始化
    /// </summary>
    public class MgrTicksStatusInited: MgrTicksStatus
    {
        public override void OnEmitAble ()
        {
            src.Enter (src.statusLoaded);
        }
    }
}