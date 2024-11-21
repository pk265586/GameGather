using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Model
{
    public static class GameConst
    {
        public const int MinDeltaHours = 2;
        public static TimeSpan MinGamesDelta { get; } = TimeSpan.FromHours(MinDeltaHours);
    }
}
