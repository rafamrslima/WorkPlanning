using System;

namespace WorkPlanning.API.DTOs
{
    public class ShiftDto
    {
        public string WorkerId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
