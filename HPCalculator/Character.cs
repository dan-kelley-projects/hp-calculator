using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCalculator
{
    // This isn't actually how a character is setup, but it is sufficient for our purposes
    class Character
    {
        // This will never have to be refactored; it is correctly encapsulated in the top level Character object
        public int ConScore { get; set; }

        // These may need to get refactored if other features are added, and are implemented as-is for simplicity
        // For example, a character could have two different classes with two different kinds of hit die
        // This would dramatically change how calculation needs to be done
        public bool HasToughness { get; set; }
        public int FavoredLevelBonus { get; set; }
        public int Levels { get; set; }
        public HitDie HitDie { get; set; }

    }
}
