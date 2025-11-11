using System.Threading.Tasks;

namespace SafeTicks
{
    /// <summary>
    /// 服务器时间戳运行环境
    /// </summary>
    public class MgrTicksEnvServer : MgrTicksEnv
    {
        /// <summary>
        /// 获取运行环境的时间戳
        /// </summary>
        /// <returns></returns>
        public override async Task<long> GetTimestampMillisecond ()
        {
            return default;
        }

        /// <summary>
        /// 事件派发 - 渡过了数秒1
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

        /// <summary>
        /// 事件派发 - 天数变化
        /// </summary>
        /// <param name="day"></param>
        public override void OnUpdateDay (float day)
        {

        }
    }
}