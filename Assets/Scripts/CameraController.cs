using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

class Camera : MonoBehaviour
{
    void LateUpdate()
    {
        // Camera follows the player
        GameObject player = GameObject.Find("Player");
        // if (player != null)
        // {
        //     Vector3 playerPosition = player.transform.position;
        //     transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        // }
        
        // Camera shake effect
        if (player && player.GetComponent<Player>().isHit)
        {
            Debug.Log("Camera shake");
            Shake();
            player.GetComponent<Player>().isHit = false;
        }
    }
    
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }
    
    private IEnumerator ShakeCoroutine()
    {
        float shakeDuration = 0.5f;
        float shakeMagnitude = 1f;
        float dampingSpeed = 1.0f;
        float shakeSpeed = 0.1f;
        float shakeTime = 0;
        
        Vector3 initialPosition = transform.position;
        float startTime = Time.time;
        
        Debug.Log("shaking at " + startTime + " for " + shakeDuration);
        while (true)
        {
            if (Time.time > startTime + shakeDuration)
            {
                Debug.Log("done shaking");
                transform.position = initialPosition;
                break;
            }

            // log 10번 흔들릴 때 마다 찍기
            if (shakeTime > 0.1f)
            {
                Debug.Log("shaking at " + Time.time + " for " + shakeDuration);
                shakeTime -= 0.1f;
            }
            
            shakeTime += Time.deltaTime;
            float x = initialPosition.x + Random.Range(-1, 1) * shakeMagnitude;
            float y = initialPosition.y + Random.Range(-1, 1) * shakeMagnitude;
            
            transform.position = new Vector3(x, y, initialPosition.z);
            yield return null;
        }
    }
}