# 前言
一般通常认为时间总是向前流动的。然而在实际生产环境中，系统时间可能因为用户误操作或其它原因往后跳跃了。这种"时间倒流"现象可能导致数据混乱、ID重复等严重问题。所以不能直接将时间戳立即同步为系统的时间戳，而是根据当前系统时间的增量，去推进安全时间戳

# 核心类说明
* SafeTimestamp：安全的时间戳，用于防止客户端时光倒流
* MgrTicks：时间戳管理器
* MgrTicksEnv：时间戳的运行环境，时间可能分为本地时间以及服务器时间

# 如何开始
以本地环境的时间为例子，初始化需要传入当前的系统时间戳

`MgrTicks.local.Init (MgrTicks.ParseTicksToTimestampMillisecond (DateTime.Now.Ticks));` 

接着通知计时器开始进行事件抛出，这一步往往在获得环境的时间戳后进行，即初始化后进行

`MgrTicks.local.OnEmitAble (); `

逐帧更新时间，需要在MonoBehaviour的Update函数中去调用

`MgrTicks.local.OnUpdate (); `

# 演示案例
Assets\Sample\Scenes\SampleScene为演示案例
