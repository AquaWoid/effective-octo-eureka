using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData 
{
    public bool dayOnePassed = false;
    public bool dayTwoPassed = false;
    public bool dayThreePassed = false;
    public bool dayFourPassed = false;
    public bool dayFivePassed = false;

    public bool bossOneDown = false;
    public bool bossTwoDown = false;
    public bool BossThreeDown = false;
    public bool bossFourDown = false;
    public bool bossFiveDown = false;

    public bool npcOneMet = false;
    public bool npcTwoMet = false;
    public bool npcThreeMet = false;
    public bool npcFourMet = false;
    public bool npcFiveMet = false;
    public WorldData (WorldStats worldStats)
    {
        dayOnePassed = worldStats.dayOnePassed;
        dayTwoPassed = worldStats.dayTwoPassed;
        dayThreePassed = worldStats.dayThreePassed;
        dayFourPassed = worldStats.dayFourPassed;
        dayFivePassed = worldStats.dayFivePassed;

        bossOneDown = worldStats.bossOneDown;
        bossTwoDown = worldStats.bossTwoDown;
        BossThreeDown = worldStats.BossThreeDown;
        bossFourDown = worldStats.bossFourDown;
        bossFiveDown = worldStats.bossFiveDown;

        npcOneMet = worldStats.npcOneMet;
        npcTwoMet = worldStats.npcTwoMet;
        npcThreeMet = worldStats.npcThreeMet;
        npcFourMet = worldStats.npcFourMet;
        npcFiveMet = worldStats.npcFiveMet;
    }

}
