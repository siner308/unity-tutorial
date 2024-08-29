using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM.Play(); // 재생
        BGM.mute = true; // 음소거
        BGM.loop = true; // 반복
        BGM.playOnAwake = true; // 자동재생
        BGM.Stop(); // 정지
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
