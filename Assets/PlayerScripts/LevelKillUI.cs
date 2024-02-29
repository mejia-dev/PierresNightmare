using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelKillUI : MonoBehaviour
{
    public TMP_Text killCountText;
    public TMP_Text levelText;
    public AudioClip levelUpSound;
    private AudioSource audioSource;

    private Leveling levelingScript;

    void Start()
{
    // Find the GameObject with the Leveling script attached
    GameObject levelingSystem = GameObject.Find("LevelingSystem");
    if (levelingSystem != null)
    {
        levelingScript = levelingSystem.GetComponent<Leveling>();
    }
    else
    {
        Debug.LogError("LevelingSystem GameObject not found. Make sure it exists in the scene.");
    }

    // Check if the UI Text objects are properly assigned
    if (killCountText == null || levelText == null)
    {
        Debug.LogError("UI Text objects not assigned.");
    }

    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
      audioSource.gameObject.AddComponent<AudioSource>();
    }

    audioSource.clip = levelUpSound;
}

    void Update()
    {
        // Update UI with data from the Leveling script
        if (levelingScript != null)
        {
            killCountText.text = "Kills: " + levelingScript.KillCount.ToString();
            levelText.text = "Level: " + levelingScript.GameLevel.ToString();
        }
    }

    public void PlayLevelUpSound()
    {
      audioSource.PlayOneShot(levelUpSound);
    }
}