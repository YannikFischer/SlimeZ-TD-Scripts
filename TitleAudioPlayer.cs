using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioPlayer : MonoBehaviour
{
    public static TitleAudioPlayer instance;
    public AudioSource TitleScreenMusic;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
