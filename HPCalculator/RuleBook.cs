using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCalculator
{
    // Encapsulates game rules
    static class RuleBook
    {
        public static GameSettings GameSettings;

        public static int CalculateHitPoints(Character character)
        {
            // Converted to doubles because fractional hit point values are rounded down only at the end of calculation
            double levelOneHitPoints = 0;
            double featBonus = 0;
            double hitDie = (double) character.HitDie;
            double levels = character.Levels;
            double conMod = CalcModFromAbilityScore(character.ConScore);

            // A character can choose to have additional hit points for each level they take in their favored class
            double favoredLevelBonus = character.FavoredLevelBonus;

            // The settings object changes how the actual rules work
            bool maxAtFirstLevel = GameSettings.MaxHitPointsAtFirstLevel;
            double multiplier = GameSettings.HitPointMultiplier;

            // Normally, its preferrable to have max hit points at first level
            if (maxAtFirstLevel)
            {
                levelOneHitPoints = (int) hitDie + conMod;
            }
            else
            {
                levels++;
            }

            // Toughness is a feat that increases a characters hit points
            // It gives three hit points plus one for every level after first
            if (character.HasToughness)
            {
                featBonus = 3 + (levels - 1);
            }

            // This is the calculation that this program encapsulates
            // In traditional gameplay, HP is rolled for each level based on the hit die. So a d12 means rolling a twelve sided die to determine next level's base HP gain.
            // This can be unfair and a character that is inteded to have a significant amount of HP (for instance barbarian) may end up rolling a 1 or 2, and end up with
            // less of an HP gain a wizard who rolled a 6.
            // By using a multiplier it averages out this discrepency so that each level is worth the same amount of HP gain, removing any unfairness from bad luck.
            double totalHitPoints = levelOneHitPoints + (levels - 1.0) * (multiplier * hitDie + conMod) + favoredLevelBonus + featBonus;
            return (int) totalHitPoints;
        }

        // The modifier is the actual value applied to checks that's derived from an ability score
        // While a score can never be negative, its modifier very much can be, as 10 is represented as an "average" score with no bonus
        // At 9, the modifier needs to be -1, then -2 at 7, -3 at 5, down until -5 at 0
        // The modifier can be infinitely posive, as well
        public static int CalcModFromAbilityScore(int score) => score / 2 - 5;

        // This doesn't really belong here but it's fine since we don't have a Die class
        // If a Die class is ever created, this should be replaced by a GetSides() method
        public static int HitDieFromString(string hitDie)
        {
            hitDie = hitDie.TrimStart('d');
            return int.Parse(hitDie);
        }
    }
}
