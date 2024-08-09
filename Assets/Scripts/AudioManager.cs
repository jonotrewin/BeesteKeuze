using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sound[] sounds;
    public bool mute = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }
    public void Play(string name)
    {
        if (mute) return;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) 
        {
            if (!s.canInterrupt) { if (s.source.isPlaying) return; }
            if (s.randomPitch)
            {
                s.source.pitch = Random.Range(s.minRandomPitch, s.maxRandomPitch);
            }
            if (s.randomClip)
            {
                s.source.clip = s.randomClips[Random.Range(0, s.randomClips.Count)];
            }
            s.source.Play();
        } 
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) s.source.Stop();
    }
}
