namespace SafeTicks
{
    /// <summary>
    /// 当前的时间状态
    /// </summary>
    public class SafeTimestampStatus {
        /// <summary>
        /// 来源
        /// </summary>
        public SafeTimestamp rel;

        /// <summary>
        /// 事件派发 - 进入状态
        /// </summary>
        public virtual void OnEnter () {

        }

        /// <summary>
        /// 事件派发 - 离开状态
        /// </summary>
        public virtual void OnExit () {

        }

        /// <summary>
        /// 事件派发 - 更新
        /// </summary>
        public virtual void OnUpdateAllOverTheWorld () {

        }
    }
}