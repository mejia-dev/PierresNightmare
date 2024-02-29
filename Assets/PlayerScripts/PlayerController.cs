using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [Header("Movement")]
    public float speed = 5f; //normal movement speed
    public float sprintMultiplier = 2f; //sprint max speed is 2x normal

    [Header("Stamina")]
    [SerializeField] //make private field visible in Inspector
    public float maxSprintTime = 5f; //how long can sprint
    public float sprintRefillRate = 1f; //stam refill
    public float sprintDepletionRate = 1f; //how quickly stam depletes
    public float acceleration = 10f; //accel to reach top speed


    private float stamina; //private backing field for stam
    public float Stamina
    {
        get => stamina;
        private set
        {
            stamina = Mathf.Clamp(value, 0, maxSprintTime); //keep stam within bounds
            // UpdateStaminaUI(); //update ui when stam changes
        }
    }

    [SerializeField, Header("Combat")]
    private float health = 100f;
    public float Health 
    { 
        get => health; 
        private set => health = value; 
    }

    [SerializeField]
    private int score; // Use SerializeField if you want it visible in the Inspector.
    public int Score 
    { 
        get => score; 
        private set => score = value; 
    }
    public float attackPower = 10f; // How hard this jacked capsule thing hits.
    public float knockbackForce = 5f; // How much force applied to enemies.

    private Rigidbody rb; //instance of rigidbody which is applied to player in unity
  
    // // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stamina = maxSprintTime; //init stamina
    }

    void Update()
    {
        HandleSprintInput();
        HandleAttackInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleSprintInput()
    {
        if (!Input.GetKey(KeyCode.LeftShift) && stamina < maxSprintTime)
        {
            stamina += sprintRefillRate * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= sprintDepletionRate * Time.deltaTime; //decrease stam when sprinting maybe include extra effort attacks at some point?
            Debug.Log("Stamina left: " + stamina);
        }
    }

    void HandleMovement()
    {
        //get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
         //create vector3 based on input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized; //normalized ensures consistent speed in all directions, but a lot of old games didn't have that, and tactics evolved around it. may be cool to remove
        Vector3 desiredVelocity = movement * speed;
        //check sprint
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            // Debug.Log("Sprinting at speed: " + (desiredVelocity * sprintMultiplier));//debug for sprint speed
            desiredVelocity *= sprintMultiplier; 
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, desiredVelocity.magnitude);
            Debug.Log("Sprinting at speed: " + rb.velocity.magnitude); //log sprint speed
        }
        //calc force to reach velocity
        Vector3 force = (desiredVelocity - rb.velocity) * acceleration;
        //Apply force to the Rigidbody
        rb.AddForce(force);
        // Ensure we don't exceed our desired velocity (especially when sprinting)
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, desiredVelocity.magnitude);

    }

    void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Change space to whatever we want attack to be
        {
            Attack();
        }
    }

    void Attack()
    {
        // Implement attack logic here
        // This could involve raycasting to detect enemies in front of the player and applying damage/knockback effect
        Debug.Log("Attack performed");
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        // Health = Mathf.Min(Health, 100f);
        // UpdateHealthUI();
        if (Health <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(float amount)
    {
        Health += amount;
        // Health = Mathf.Min(Health, 100f);
        // UpdateHealthUI();
    }

    public void AddScore(int amount)
    {
        Score += amount;
        // UpdateScoreUI();
    }

    void Die()
    {
        Debug.Log("Player Died");
        //Handle player death. Disable controls, show end screen, maybe a score
    }

    // private void UpdateStaminaUI()
    // {
    //     //notify ui stam
    // }

    // private void UpdateHealthUI()
    // {
    //     //notify ui health
    // }

    // private void UpdateScoreUI()
    // {
    //     //notify ui score
    // }

    // Example of how to apply knockback to an enemy when attacked
    // This would be called on the enemy's Rigidbody when they take damage from an attack
    public void ApplyKnockback(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
  
}
