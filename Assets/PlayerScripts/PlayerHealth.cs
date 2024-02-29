using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    [SerializeField, Header("Combat")]
    private bool isAlive = true; // Use a private field for backing the property
    public Image hpBarFill;
    public PlayerEffects playerEffects;
    private AudioSource audioSource;
    [SerializeField]
    public AudioClip playerDeathSound;
    public DeathScreenManager deathScreenManager;

    private int level = 1;
    public int Level
    {
        get => level;
        private set => level = value;
    }

    private float attackPower = 10f;
    public float AttackPower
    {
        get => attackPower;
        private set => attackPower = value;
    }

    private float knockbackForce = 5f;
    public float KnockbackForce
    {
        get => knockbackForce;
        private set => knockbackForce = value;
    }
    
    public bool IsAlive
    {
        get => isAlive; // Correctly reference the private field
        private set => isAlive = value; // Allow setting within the class
    }
    public float health;
    public float maxHealth = 100f;
    // public float Health 
    // { 
    //     get => health; 
    //     private set => health = value; 
    // }

    [SerializeField]
    private int score; // Use SerializeField if you want it visible in the Inspector.
    public int Score
    {
        get => score;
        private set => score = value;
    }
    // public float attackPower = 10f; // How hard this jacked capsule thing hits.
    // public float knockbackForce = 5f; // How much force applied to enemies.

    void Awake()
    {
        isAlive = true;
        playerEffects = GetComponent<PlayerEffects>();
        audioSource = GetComponent<AudioSource>();
        deathScreenManager = FindObjectOfType<DeathScreenManager>(); 

        if (playerEffects == null)
        {
            Debug.LogError("PlayerEffects component not found on the GameObject");
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on GameObj (playerHealth script)");
        }

        if (deathScreenManager == null)
        {
            Debug.LogWarning("DeathScreenManager component not found in scene.");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (!isAlive) return; //prevent death scream spamming

        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (playerEffects != null)
            {
                playerEffects.TakeDamage();
            }
            else
            {
                Debug.LogError("PlayerEffects script not assigned on PlayerHealth script.");
            }
        }
        UpdateHPBar();
    }

    void UpdateHPBar()
    {
        if (hpBarFill != null)
        {
            float fillAmount = health / maxHealth;
            hpBarFill.fillAmount = fillAmount;
            Debug.Log($"Updated HP Bar Fill Amount: {fillAmount}");
        }
        else
        {
            Debug.LogError("HP Bar Fill Image not assigned in PlayerHealth script.");
        }
    }

    void Die()
    {
        isAlive = false;
        Debug.Log("Player died. Should not be able to move anymore");

        if (playerDeathSound != null)
        {
            audioSource.PlayOneShot(playerDeathSound);
        }

        //death screen
        if (deathScreenManager != null)
        {
            deathScreenManager.ShowDeathScreen();
        }
        else
        {
            Debug.LogError("DeathScreenManager not found.");
        }
        //this should allow the player to restart the game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    void Start()
    {
        isAlive = true;
        health = maxHealth;
        UpdateHPBar();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseAttributes()
    {
        level++;

        attackPower += 5f;
        knockbackForce += 5f;

        //heal player
        health += 25;
        health = Mathf.Clamp(health, 0, maxHealth);

        Debug.Log($"Level Up! New level: {level}, New Attack Power: {attackPower}, New Knockback Force: {knockbackForce}");

        UpdateHPBar();
        
        //communicate w FPC
        if (firstPersonController != null)
        {
            firstPersonController.UpdateAttributes(attackPower, knockbackForce);
        }
        else
        {
            Debug.LogError("FirstPersonController reference not set in PlayerHealth");
        }
    }
}
