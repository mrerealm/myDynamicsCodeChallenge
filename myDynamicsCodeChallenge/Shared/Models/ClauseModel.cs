using System;
using myDynamicsCodeChallenge.Shared.Enumerations;

namespace myDynamicsCodeChallenge.Shared.Models
{
    public class ClauseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ClausePositions Position { get; set; }
    }
}
