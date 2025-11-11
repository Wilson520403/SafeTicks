namespace SafeTicks {

    /// <summary>
    /// 当前时间戳的状态
    /// </summary>
    public class MgrTicksStatus {

        /// <summary>
        /// 来源
        /// </summary>
        public MgrTicks src;

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
        public virtual void OnUpdate () {

        }

        /// <summary>
        /// 事件派发 - 初始化完毕
        /// </summary>
        public virtual void OnInited ()
        {

        }

        /// <summary>
        /// 事件派发 - 得到外派事件的许可
        /// </summary>
        public virtual void OnEmitAble ()
        {

        }
    }
}