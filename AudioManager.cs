using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource slimeyDestroy;
    public AudioSource inGameMusic;
    public AudioSource titleScreenMusic;
    public AudioSource gameOverMusic;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
