using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoomController : ItemController
{
    // Update is called once per frame
    protected override void ItemGain()
    {
        if (playerController.boomCount < 3)
        {
            playerController.boomCount++;
            Debug.Log("boomCount: " + playerController.boomCount);
            UIController.Instance.UpdateBoomCount();
        }
    }
}
