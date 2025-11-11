using System;

namespace SafeTicks
{
    /// <summary>
    /// 时间戳管理，用于缓存服务器时间戳以及自动同步服务器时间戳
    /// </summary>
    public class MgrTicks
    {
        /// <summary>
        /// 本地实例
        /// </summary>
        public static MgrTicks local = new (new MgrTicksEnvLocal ());

        /// <summary>
        /// 服务器实例
        /// </summary>
        public static MgrTicks server = new (new MgrTicksEnvServer ());

        /// <summary>
        /// 运行环境
        /// </summary>
        public MgrTicksEnv env;

        /// <summary>
        /// 安全的时间戳
        /// </summary>
        public SafeTimestamp safeTimestamp;

        /// <summary>
        /// 服务器与本地时间戳的差值
        /// </summary>
        public long ticksServerSubLocal;

        /// <summary>
        /// 状态 - 尚未初始化
        /// </summary>
        public MgrTicksStatusStart statusStart;
        /// <summary>
        /// 状态 - 初始化完毕，等待许可
        /// </summary>
        public MgrTicksStatusInited statusInited;
        /// <summary>
        /// 状态 - 正在同步时间戳
        /// </summary>
        public MgrTicksStatusLoading statusLoading;
        /// <summary>
        /// 状态 - 时间戳已同步完毕
        /// </summary>
        public MgrTicksStatusLoaded statusLoaded;

        public MgrTicks (MgrTicksEnv env)
        {
            // 绑定运行环境
            this.env = env;

            env.Init (this);

            safeTimestamp = new ()
            {
                src = this
            };

            statusStart = new ()
            {
                src = this
            };

            statusInited = new ()
            {
                src = this
            };

            statusLoading = new ()
            {
                src = this
            };

            statusLoaded = new ()
            {
                src = this
            };

            Enter (statusStart);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init (long timestampMillisecond)
        {
            // 获取标准时间戳
            long ticks = ParseTimestampMillisecondToTicks (timestampMillisecond);

            // 记录下差值
            ticksServerSubLocal = ticks - DateTime.Now.Ticks;

            // 告知起始时间戳
            safeTimestamp.OnInit (ticks);

            // 表明已经初始化完毕
            currStatus.OnInited ();
        }

        /// <summary>
        /// 毫秒时间戳转 Ticks
        /// </summary>
        /// <param name="timestampMillisecond"></param>
        /// <returns></returns>
        public static long ParseTimestampMillisecondToTicks (long timestampMillisecond)
        {
            return timestampMillisecond * 10000 + new DateTime (1970 , 1 , 1 , 0 , 0 , 0).Ticks;
        }

        /// <summary>
        /// Ticks 转毫秒时间戳
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static long ParseTicksToTimestampMillisecond (long ticks)
        {
            return (ticks - new DateTime (1970 , 1 , 1 , 0 , 0 , 0).Ticks) / 10000;
        }

        /// <summary>
        /// 事件派发 - 桥接事件已初始化完毕
        /// </summary>
        public void OnEmitAble ()
        {
            currStatus.OnEmitAble ();
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public MgrTicksStatus currStatus;

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="statua"></param>
        public void Enter (MgrTicksStatus status)
        {
            MgrTicksStatus rec = currStatus;
            currStatus = status;
            rec?.OnExit ();
            currStatus.OnEnter ();
        }

        /// <summary>
        /// 事件派发 - 更新
        /// </summary>
        public void OnUpdate ()
        {
            currStatus.OnUpdate ();
        }


        public void GetTimeStampNextDay ()
        {

        }
    }
}