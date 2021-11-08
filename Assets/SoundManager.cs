using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [System.Serializable]
    public class SoundType
    {
        public Sound sound;
        public AudioClip clip;
    }
    public static SoundManager Instance = null;

    [SerializeField] List<SoundType> sounds;

    [Header("Component References")]
    [SerializeField] AudioSource source;
   


    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

 public void Play(Sound s)
    {
        if (s == Sound.Scream)
        {
            source.loop = false;
        }
        source.clip= sounds.Find(x => x.sound == s).clip;
        source.Play();
        source.volume = 0.6f;
       
    }
}
