using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevel : MonoBehaviour
{
    public TMP_Text killCountText;
    public TMP_Text levelText;

    private Leveling levelingScript;

    // void Start()
    // {
    //     GameObject player = GameObject.FindWithTag("Player"); // Assuming your Leveling script is attached to the player
    //     if (player != null)
    //     {
    //         levelingScript = player.GetComponent<Leveling>();
    //     }
    // }

    void Start()
{
    killCountText = GameObject.Find("KillCount").GetComponent<TMP_Text>();
    levelText = GameObject.Find("PlayerLevel").GetComponent<TMP_Text>();
}

    void Update()
    {
        if (levelingScript != null)
        {
            killCountText.text = "Kills: " + levelingScript.KillCount.ToString();
            levelText.text = "Level: " + levelingScript.GameLevel.ToString();
        }
    }
}