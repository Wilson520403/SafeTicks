using System;
using UnityEngine;

namespace SafeTicks
{
    /// <summary>
    /// 安全的时间戳，用于防止客户端时光倒流，基于 1970
    /// </summary>
    public class SafeTimestamp
    {
        /// <summary>
        /// 来源
        /// </summary>
        public MgrTicks src;

        /// <summary>
        /// 当前系统的时间戳 - 不安全，完全取自于环境，可能前后跃迁
        /// </summary>
        public long ticksSysEnv;

        long _timeStampTicks;

        /// <summary>
        /// 微秒时间戳
        /// </summary>
        public long TimeStampTicks
        {
            get
            {
                return _timeStampTicks;
            }
        }

        long _timeStampMS;

        /// <summary>
        /// 当前毫秒级时间戳
        /// </summary>
        public long TimeStampMS
        {
            get
            {
                return _timeStampMS;
            }
        }

        long _timeStampSec;

        /// <summary>
        /// 当前秒级时间戳
        /// </summary>
        public long TimeStampSec => _timeStampSec;

        /// <summary>
        /// 安全的时间戳
        /// </summary>
        long _ticksSafe;

        /// <summary>
        /// 安全的时间戳
        /// </summary>
        public long TicksSafe
        {
            get
            {
                return _ticksSafe;
            }
            set
            {
                _ticksSafe = value;
                DateTime ticksSafeDateTime = new (_ticksSafe);
                TimeSpan st = ticksSafeDateTime - new DateTime (1970 , 1 , 1 , 0 , 0 , 0);
                _timeStampTicks = st.Ticks;
                _timeStampMS = st.Ticks / 10000;
                _timeStampSec = _timeStampMS / 1000;
            }
        }

        /// <summary>
        /// 正常情况
        /// </summary>
        public SafeTimestampStatusOrdinary statusOrdinary;

        /// <summary>
        /// 发生了倒流，正在修复
        /// </summary>
        public SafeTimestampStatusReverse statusReverse;

        public void OnInit (long ticks)
        {
            statusOrdinary = new ()
            {
                rel = this
            };

            statusReverse = new ()
            {
                rel = this
            };

            Enter (statusOrdinary);

            ticksSysEnv = ticks;
            currStatus.OnUpdateAllOverTheWorld ();
            _idMs = TicksSafe / SafeTimestampDefine.TICKS_MILLISECOND;
            _idSeconds = TicksSafe / SafeTimestampDefine.TICKS_SECOND;
            _idDays = TicksSafe / SafeTimestampDefine.TICKS_DAY;
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public SafeTimestampStatus currStatus;

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="status"></param>
        public void Enter (SafeTimestampStatus status)
        {
            var rec = currStatus;
            currStatus = status;
            rec?.OnExit ();
            currStatus.OnEnter ();
        }

        /// <summary>
        /// 起始时间戳 - 毫秒
        /// </summary>
        long _idMs;
        /// <summary>
        /// 起始时间戳 - 秒
        /// </summary>
        long _idSeconds;
        /// <summary>
        /// 起始时间 - 天
        /// </summary>
        long _idDays;

        /// <summary>
        /// 事件派发 - 更新
        /// </summary>
        public void OnUpdate (long ticks)
        {
            // 不稳定的外部时间戳
            ticksSysEnv = ticks;

            // 这一步更新，确保 ticksSafe 前进
            currStatus.OnUpdateAllOverTheWorld ();

            // 毫秒的事件
            long idMs = TicksSafe / SafeTimestampDefine.TICKS_MILLISECOND;
            if (_idMs != idMs)
            {
                long passed = idMs - _idMs;
                _idMs = idMs;
                src.env.OnUpdatedMillisecond (passed);
            }

            // 秒的事件
            long idSeconds = TicksSafe / SafeTimestampDefine.TICKS_SECOND;
            if (_idSeconds != idSeconds)
            {
                long passed = idSeconds - _idSeconds;
                _idSeconds = idSeconds;
                src.env.OnUpdatedSecond (passed);
            }

            // 天的事件
            long idDays = TicksSafe / SafeTimestampDefine.TICKS_DAY;
            if (_idDays != idDays)
            {
                long passed = idDays - _idDays;
                _idDays = idDays;
                src.env.OnUpdateDay (passed);
            }
        }

        /// <summary>
        /// 把目标时间戳转为倒计时文本
        /// </summary>
        /// <param name="msTarget"></param>
        /// <returns></returns>
        public string MsTargetToTxtShort (long msTarget)
        {
            return TicksLessToTxtShort ((msTarget - TimeStampMS) * SafeTimestampDefine.TICKS_MILLISECOND);
        }

        /// <summary>
        /// 从某时间戳开始累计的时间文本
        /// </summary>
        /// <param name="msFrom"></param>
        /// <returns></returns>
        public string MsFromToTxtShort (long msFrom)
        {
            return TicksLessToTxtShort ((TimeStampMS - msFrom) * SafeTimestampDefine.TICKS_MILLISECOND);
        }

        /// <summary>
        /// 把目标时间戳转为倒计时文本
        /// </summary>
        /// <param name="ticksTarget"></param>
        /// <returns></returns>
        public string TicksTargetToTxtShort (long ticksTarget)
        {
            return TicksLessToTxtShort (ticksTarget - TimeStampMS);
        }

        /// <summary>
        /// 从某时间戳开始累计的时间文本
        /// </summary>
        /// <param name="ticksFrom"></param>
        /// <returns></returns>
        public string TicksFromToTxtShort (long ticksFrom)
        {
            return TicksLessToTxtShort (TicksSafe - ticksFrom);
        }

        /// <summary>
        /// 把目标时间戳转为倒计时文本
        /// </summary>
        /// <param name="ticksTarget"></param>
        /// <returns></returns>
        public string TicksTargetToTxtTotal (long ticksTarget)
        {
            return TicksLessToTxtTotal (ticksTarget - TicksSafe);
        }

        /// <summary>
        /// 把剩余毫秒转为倒计时文本
        /// </summary>
        /// <param name="ticksLess"></param>
        /// <returns></returns>
        public string TicksLessToTxtTotal (long ticksLess)
        {
            if (ticksLess < 1)
            {
                return "";
            }

            // 时间戳
            var ts = ticksLess % 10000;
            // 毫秒
            ticksLess /= 10000;
            var ms = ticksLess % 1000;
            // 秒
            ticksLess /= 1000;
            long second = ticksLess % 60;
            // 分
            ticksLess /= 60;
            long minute = ticksLess % 60;
            // 时
            ticksLess /= 60;
            long hour = ticksLess % 24;
            // 天
            ticksLess /= 24;
            long day = ticksLess;

            // 天、时、分、秒
            return $"{day}d {hour}h {minute}m {second}s";
        }

        /// <summary>
        /// 把剩余毫秒转为倒计时文本
        /// </summary>
        /// <param name="ticksLess"></param>
        /// <returns></returns>
        public string TicksLessToTxtShort (long ticksLess)
        {
            if (ticksLess < 1)
            {
                return "";
            }
            ;

            var ts = ticksLess % 10000;
            // 毫秒
            ticksLess /= 10000;
            var ms = ticksLess % 1000;
            // 秒
            ticksLess /= 1000;
            long second = ticksLess % 60;
            // 分
            ticksLess /= 60;
            long minute = ticksLess % 60;
            // 时
            ticksLess /= 60;
            long hour = ticksLess % 24;
            // 天
            ticksLess /= 24;
            long day = ticksLess;

            // 天
            if (0 < day)
            {
                return $"{day}d {hour}h";
            }
            ;

            // 时
            if (0 < hour)
            {
                return $"{hour}h {minute}m";
            }
            ;

            // 分
            return $"{minute}m {second}s";
        }

        /// <summary>
        /// 到下一天的时间单位数量
        /// </summary>
        /// <param name="ticksOffsetCurrent"></param>
        /// <param name="ticksOffsetTarget"></param>
        /// <returns></returns>
        public long TicksNextDay (long ticksOffsetCurrent , long ticksOffsetTarget)
        {
            var offsetTotal = ticksOffsetCurrent - ticksOffsetTarget;
            return ((TimeStampTicks + offsetTotal) / SafeTimestampDefine.TICKS_DAY + 1) * SafeTimestampDefine.TICKS_DAY - offsetTotal + SafeTimestampDefine.TIME_START_TICKS;
        }

        /// <summary>
        /// 到下周一的时间单位数量
        /// </summary>
        /// <param name="ticksOffsetCurrent"></param>
        /// <param name="ticksOffsetTarget"></param>
        /// <returns></returns>
        public long TicksNextWeek (long ticksOffsetCurrent , long ticksOffsetTarget)
        {
            var offsetTotal = ticksOffsetCurrent - ticksOffsetTarget + SafeTimestampDefine.TICKS_DAY * 3;
            return ((TimeStampTicks + offsetTotal) / SafeTimestampDefine.TICKS_WEEK + 1) * SafeTimestampDefine.TICKS_WEEK - offsetTotal + SafeTimestampDefine.TIME_START_TICKS;
        }
    }
}