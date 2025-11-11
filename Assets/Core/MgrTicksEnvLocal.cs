using System;
using System.Threading.Tasks;

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
            return MgrTicks.ParseTicksToTimestampMillisecond (DateTime.Now.Ticks);
        }

        /// <summary>
        /// 事件派发 - 渡过了数秒
        /// </summary>
        /// <param name="passed"></param>
        public override void OnUpdatedSecond (float passed)
        {

        }

        /// <summary>
        /// 事件派发 - 渡过了数毫秒
        /// </summary>
        /// <param name="passed"></param>
        public override void OnUpdatedMillisecond (float passed)
        {

        }
    }
}