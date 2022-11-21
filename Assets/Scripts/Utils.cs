using System;

namespace RaceGame.Utils
{
    public static class TimerUtils
    {
        public static string MillisecondsToRaceTimeString(float milliseconds)
        {
            var dateTime = new DateTime(TimeSpan.FromMilliseconds(milliseconds).Ticks);
            return dateTime.ToString("mm:ss:fff");
        }
    }
}