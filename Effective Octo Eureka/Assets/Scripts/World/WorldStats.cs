using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {

        SaveSystem.SaveWorld(this);

    }

    public void LoadGame()
    {

        WorldData data = SaveSystem.LoadWorld();

        dayOnePassed = data.dayOnePassed;
        dayTwoPassed = data.dayTwoPassed;
        dayThreePassed = data.dayThreePassed;
        dayFourPassed = data.dayFourPassed;
        dayFivePassed = data.dayFivePassed;

        bossOneDown = data.bossOneDown;
        bossTwoDown = data.bossTwoDown;
        BossThreeDown = data.BossThreeDown;
        bossFourDown = data.bossFourDown;
        bossFiveDown = data.bossFiveDown;

        npcOneMet = data.npcOneMet;
        npcTwoMet = data.npcTwoMet;
        npcThreeMet = data.npcThreeMet;
        npcFourMet = data.npcFourMet;
        npcFiveMet = data.npcFiveMet;

    }
}
