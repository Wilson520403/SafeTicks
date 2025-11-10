using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SafeTicks
{
    public class MgrTimestampDefine
    {
        /// <summary>
        /// 每毫秒的 Tick 数量
        /// </summary>
        public const long TICKS_MILLISECOND = 10000;

        /// <summary>
        /// 每秒的 Tick 数量
        /// </summary>
        public const long TICKS_SECOND = 1000 * TICKS_MILLISECOND;

        /// <summary>
        /// 每分钟的 Tick 数量
        /// </summary>
        public const long TICKS_MINUTE = 60 * TICKS_SECOND;

        /// <summary>
        /// 每小时的 Tick 数量
        /// </summary>
        public const long TICKS_HOUR = 60 * TICKS_MINUTE;

        /// <summary>
        /// 每天的 Tick 数量
        /// </summary>
        public const long TICKS_DAY = 24 * TICKS_HOUR;

        /// <summary>
        /// 每周的 Tick 数量
        /// </summary>
        public const long TICKS_WEEK = TICKS_DAY * 7;

        /// <summary>
        /// 起始时间
        /// </summary>
        public static DateTime TIME_START = new (
            year: 1970 ,
            month: 1 ,
            day: 1 ,
            hour: 0 ,
            minute: 0 ,
            second: 0 ,
            kind: DateTimeKind.Utc
        );

        /// <summary>
        /// 起始时间的时间戳
        /// </summary>
        public static long TIME_START_TICKS = TIME_START.Ticks;
    }
}