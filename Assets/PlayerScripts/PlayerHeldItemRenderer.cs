using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldItemRenderer : MonoBehaviour
{
    private GameObject playerGameObj;
    private Weapon selectedWeapon;

    [SerializeField]
    private GameObject[] weaponModels;
    void Start()
    {
        playerGameObj = GameObject.Find("FirstPersonController");
        if (playerGameObj != null)
        {
            selectedWeapon = playerGameObj.GetComponent<FirstPersonController>().currentWeapon;
        }
    }

    void Update()
    {
        selectedWeapon = playerGameObj.GetComponent<FirstPersonController>().currentWeapon;
        if (selectedWeapon.name == "Fist") 
        {
            foreach (GameObject weaponModel in weaponModels)
            {
                weaponModel.SetActive(false);
                if (weaponModel.name == "fist")
                {
                    weaponModel.SetActive(true);
                }
            }
        }

        if (selectedWeapon.name == "Gun") 
        {
            foreach (GameObject weaponModel in weaponModels)
            {
                weaponModel.SetActive(false);
                if (weaponModel.name == "gun")
                {
                    weaponModel.SetActive(true);
                }
            }
        }

        if (selectedWeapon.name == "Bat") 
        {
            foreach (GameObject weaponModel in weaponModels)
            {
                weaponModel.SetActive(false);
                if (weaponModel.name == "bat")
                {
                    weaponModel.SetActive(true);
                }
            }
        }
    }
}
