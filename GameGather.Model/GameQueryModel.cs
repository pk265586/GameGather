using System;

namespace GameGather.Model
{
    public class GameQueryModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SportType { get; set; }
        public string? Name { get; set; }
        public string? Team { get; set; }
        public int RowCount { get; set; }
    }
}
