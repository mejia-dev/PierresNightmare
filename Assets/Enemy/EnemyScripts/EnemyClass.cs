using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClass : MonoBehaviour
{
    public GameObject playerGameObj;
    public GameObject levelingSystem;

    private bool IsAlive;
    private float health = 100f;

    // Attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float attackDamage;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public LayerMask groundLayer, playerLayer;

    PlayerHealth playerHealth;

    // PATROL (with waypoints)
    // [SerializeField] GameObject[] waypoints;
    // int currentWaypoint = 0;
    [SerializeField] float followSpeed = 1f;


    // PATROL (without waypoints)
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;



    private void Awake()
    {
        Debug.Log("An enemy has awoken");
        IsAlive = true;
        playerGameObj = GameObject.Find("FirstPersonController");
        if (playerGameObj != null)
        {
            playerHealth = playerGameObj.GetComponent<PlayerHealth>();
        }
        levelingSystem = GameObject.Find("LevelingSystem");
    }

    // PATROL (with waypoints)
    // private void Patroling()
    // {
    //     if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < .1f)
    //     {
    //         currentWaypoint++;
    //         if (currentWaypoint >= waypoints.Length)
    //         {
    //             currentWaypoint = 0;
    //         }
    //     }

    //     transform.LookAt(waypoints[currentWaypoint].transform);
    //     transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, followSpeed * Time.deltaTime);
    // }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkablePoints();

        if (walkPointSet) transform.position = Vector3.MoveTowards(transform.position, walkPoint, followSpeed * Time.deltaTime);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkablePoints()
    {
        float ranX = Random.Range(-walkPointRange, walkPointRange);
        float ranZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + ranX, transform.position.y, transform.position.z + ranZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;

    }

    private void ChasePlayer()
    {
        transform.LookAt(playerGameObj.transform);
        transform.position = Vector3.MoveTowards(transform.position, playerGameObj.transform.position, followSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        // this line might actually need to be used to stop the enemy from moving, if ranged. Otherwise, it just keeps moving the enemy towards the player.
        transform.position = Vector3.MoveTowards(transform.position, playerGameObj.transform.position, followSpeed * Time.deltaTime);

        transform.LookAt(playerGameObj.transform);
        

        if (!alreadyAttacked)
        {
            Debug.Log(gameObject.name + " attacked player at: " + Time.time);
            // This is the attack
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage); // Call function in PlayerHealth to take damage
        }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Disable enemy, play death anim, or remove game object
        Debug.Log("Enemy died");
        gameObject.SetActive(false);
        levelingSystem.GetComponent<Leveling>().AddKill();
    }

    private void ResetAttack()
    {
        Debug.Log(gameObject.name + " can attack again.");
        alreadyAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
}
