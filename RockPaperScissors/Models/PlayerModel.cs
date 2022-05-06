using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RockPaperScissors.Models.Moves;

namespace RockPaperScissors.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public AvailableMoves Move { get; set; }
    }
}
