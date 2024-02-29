using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leveling : MonoBehaviour
{
    private int gameLevel;
    public int GameLevel
    { get => gameLevel; }

    private int killCount;
    public int KillCount
    { get => killCount; }

    private int killCountGoal;

    public LevelKillUI levelKillUI;
    
    public PlayerHealth playerHealth;

    void Awake()
    {
        gameLevel = 1;
        killCountGoal = 3;
    }

    void Update()
    {
        if (killCount == killCountGoal)
        {
            LevelUp();
            killCountGoal = killCountGoal + killCountGoal;
            //get jacked!!!
            playerHealth.IncreaseAttributes();
        }
    }

    public void LevelUp()
    {
        gameLevel++;
        if (levelKillUI != null)
        {
            levelKillUI.PlayLevelUpSound();
        }
    }

    public void AddKill()
    {
        Debug.Log("Got a kill");
        killCount++;
    }

}
