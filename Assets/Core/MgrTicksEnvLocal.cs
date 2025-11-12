using System.Threading.Tasks;
using Sample;
using UnityEngine;

namespace SafeTicks
{
    /// <summary>
    /// 本地时间戳运行环境
    /// </summary>
    public class MgrTicksEnvLocal : MgrTicksEnv
    {
        /// <summary>
        /// 获取运行环境的时间戳
        /// </summary>
        /// <returns></returns>
        public override async Task<long> GetTimestampMillisecond ()
        {
            return MgrTicks.ParseTicksToTimestampMillisecond (System.DateTime.Now.Ticks);
        }

        float _sec;
        float _millisec;

        /// <summary>
        /// 事件派发 - 渡过了数秒
        /// </summary>
        /// <param name="passed"></param>
        public override void OnUpdatedSecond (float passed)
        {
            _sec += passed;
            mgrTicks.OnUpdatedSecond (_sec);
        }

        /// <summary>
        /// 事件派发 - 渡过了数毫秒
        /// </summary>
        /// <param name="passed"></param>
        public override void OnUpdatedMillisecond (float passed)
        {
            _millisec += passed;
            mgrTicks.OnUpdatedMillisecond (_millisec);
        }
    }
}