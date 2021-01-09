# HP Calculator
A Windows desktop application used to automatically calculate HP gains from one level to the next for Pathfinder. This can be altered for other d20 games.

## Calculation
<img src="https://latex.codecogs.com/png.latex?\dpi{100}&space;\bg_white&space;\small&space;HP&space;=&space;firstLevelHP&space;&plus;&space;(level&space;-&space;1)&space;\times&space;(multiplier&space;\times&space;hitDie&space;&plus;&space;conMod&space;&plus;&space;bonuses)" title="\small HP = firstLevelHP + (level - 1) \times (multiplier \times hitDie + conMod + bonuses)" />
Normally, when HP is calculated a "hit die" or HD is rolled. Since it's a die ranging in sides from 6 - 12, this means the possible values are anywhere from 1 to 12. The use of a multiplier to smooths out luck-based HP generation.

This tool also encapsulates bonuses from favored class level and the Toughness feat from the Core Rulebook.