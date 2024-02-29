using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpController : MonoBehaviour
{
    public Weapon gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange, dropForwardForce, dropUpwardForce;
    public bool Equipped;
    public static bool slotFull;

    private void Update()
    {
        // pick up
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!Equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        // drop
        if (Equipped && Input.GetKeyDown(KeyCode.R)) Drop();
    }

    private void PickUp()
    {
        Equipped = true;
        slotFull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

        // gun enabled = true;
    }
    
    private void Drop()
    {
        Equipped = false;
        slotFull = false;

        rb.isKinematic = false;
        coll.isTrigger = false;

        // gun enabled = false;
    }
}
