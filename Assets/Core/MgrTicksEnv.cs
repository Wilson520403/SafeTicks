using System.Threading.Tasks;

namespace SafeTicks
{
    /// <summary>
    /// 时间戳管理器的运行环境
    /// </summary>
    public class MgrTicksEnv
    {
        /// <summary>
        /// 绑定的时间戳管理
        /// </summary>
        public MgrTicks mgrTicks;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mgrTicks"></param>
        public void Init (MgrTicks mgrTicks)
        {
            this.mgrTicks = mgrTicks;
        }

        /// <summary>
        /// 获取运行环境的时间戳
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> GetTimestampMillisecond( )
        {
            return 0;
        }

        /// <summary>
        /// 事件派发 - 渡过了数秒
        /// </summary>
        /// <param name="second"></param>
        public virtual void OnUpdatedSecond (float second)
        {

        }

        /// <summary>
        /// 事件派发 - 渡过了数毫秒
        /// </summary>
        /// <param name="millisecond"></param>
        public virtual void OnUpdatedMillisecond (float millisecond)
        {

        }

        /// <summary>
        /// 事件派发 - 渡过了数天
        /// </summary>
        /// <param name="day"></param>
        public virtual void OnUpdateDay (float day)
        {

        }
    }
}