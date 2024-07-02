using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioSource _audioSource;
    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;

        var audioClips = Resources.LoadAll<AudioClip>("2D_SE");
        foreach(var clip in audioClips)
        {
            _clips.Add(clip.name, clip);
        }
    }
    public void Play(string clipname) 
    {
        if (!_clips.ContainsKey(clipname))
        {
            throw new Exception("Sound" + clipname + "is not defined");
        }

        _audioSource.clip = _clips[clipname];
        _audioSource.Play();
    }
}
