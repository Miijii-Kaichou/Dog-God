using System;
using UnityEngine;
public class CleasingPosition
{
    public int month, day, year, hour, minute, second;
    public TimeZoneInfo timeZone;
    public static CleasingPosition zero = new CleasingPosition(0, 0, 0, 0, 0, 0, TimeZoneInfo.Local);
    public object[] date;
    public const int MONTH_INDEX = 0;
    public const int DAY_INDEX = 1;
    public const int YEAR_INDEX = 2;
    public const int HOUR_INDEX = 3;
    public const int MINUTE_INDEX = 4;
    public const int SECOND_INDEX = 5;
    public DateTime Date
    {
        get
        {
            return new DateTime((int)date[YEAR_INDEX], (int)date[MONTH_INDEX], (int)date[DAY_INDEX], (int)date[HOUR_INDEX], (int)date[MINUTE_INDEX], (int)date[SECOND_INDEX]);
        }
    }
    public CleasingPosition(int month, int day, int year, int hour, int minute, int second, TimeZoneInfo timeZone)
    {
        this.month = month;
        this.day = day;
        this.year = year;
        this.hour = hour;
        this.minute = minute;
        this.second = second;
        this.timeZone = timeZone;

        date = new object[7]
        {
            month,
            day,
            year,
            hour,
            minute,
            second,
            timeZone
        };
    }

    public static CleasingPosition operator +(CleasingPosition a, CleasingPosition b)
    {
        DateTime dateTimeA = new DateTime(a.year, a.month, a.day, a.hour, a.minute, a.second, DateTimeKind.Local);
        DateTime dateTimeB = new DateTime(b.year, b.month, b.day, b.hour, b.minute, b.second, DateTimeKind.Local);

        DateTime newDateTime = new DateTime(dateTimeA.Ticks + dateTimeB.Ticks);
        CleasingPosition newCleasingPosition = new CleasingPosition(newDateTime.Month, newDateTime.Day, newDateTime.Year, newDateTime.Hour, newDateTime.Minute, newDateTime.Second, TimeZoneInfo.Local);
        return newCleasingPosition;
    }

    public static CleasingPosition operator -(CleasingPosition a, CleasingPosition b)
    {
        DateTime dateTimeA = new DateTime(a.year, a.month, a.day, a.hour, a.minute, a.second, DateTimeKind.Local);
        DateTime dateTimeB = new DateTime(b.year, b.month, b.day, b.hour, b.minute, b.second, DateTimeKind.Local);

        DateTime newDateTime = new DateTime((long)Mathf.Max(dateTimeA.Ticks, dateTimeB.Ticks) - (long)Mathf.Min(dateTimeA.Ticks, dateTimeB.Ticks));
        CleasingPosition newCleasingPosition = new CleasingPosition(newDateTime.Month, newDateTime.Day, newDateTime.Year, newDateTime.Hour, newDateTime.Minute, newDateTime.Second, TimeZoneInfo.Local);
        return newCleasingPosition;
    }
}
