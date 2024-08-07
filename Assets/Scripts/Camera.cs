using System;
using UnityEngine;

class Camera : MonoBehaviour
{
    private int _frame = 0;

    void LateUpdate()
    {
        // Camera follows the player
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        }
    }
}