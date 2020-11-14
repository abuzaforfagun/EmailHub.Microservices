using System.Collections.Generic;

namespace EmailProcessor.Services
{
    public class PepipostError
    {
        public List<PepipostErrorEntry> Error { get; set; }
        public string Description { get; set; }
    }
    public class PepipostErrorEntry
    {
        public string Message { get; set; }
        public string Field { get; set; }
    }
}