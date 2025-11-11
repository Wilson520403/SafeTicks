using UnityEngine;

namespace SafeTicks
{
    /// <summary>
    /// 尚未初始化
    /// </summary>
    public class MgrTicksStatusStart: MgrTicksStatus
    {
        public override void OnInited()
        {
            src.Enter (src.statusInited);
        }

        public override void OnEmitAble ()
        {
            Debug.LogError ("MgrTicks Should OnInited Before OnEmitAble!");
        }
    }
}