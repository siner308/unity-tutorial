using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : ItemController
{
    protected override void ItemGain()
    {
        playerController = player.GetComponent<PlayerController>();
        if (playerController.damage < 3)
        {
            playerController.damage++;
            Debug.Log("damage: " + playerController.damage);
        }
    }
}
