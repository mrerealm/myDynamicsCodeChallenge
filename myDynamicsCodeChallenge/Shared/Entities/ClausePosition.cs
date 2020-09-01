using System;
namespace myDynamicsCodeChallenge.Shared.Aggregates
{
    public class ClausePosition
    {
        public int Id { get; set; }
        public int ClauseId { get; set; }
        public int PositionId { get; set; }
        public int? Order { get; set; }
    }
}
