using System;

namespace GameGather.Model
{
    public static class GameConst
    {
        public const int MinDeltaHours = 2;
        public static TimeSpan MinGamesDelta { get; } = TimeSpan.FromHours(MinDeltaHours);
    }
}
