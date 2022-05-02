using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using Random=UnityEngine.Random;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.time = s.time;
        }
    }

    void Start()
    {
        Play("MenuSound");
    }

    public void Play(string name)
    { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayPitched(string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = (Random.Range(0.8f, 1.2f));
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public IEnumerator FadeInCor(string name, float NewVolume, float FadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.volume = 0f;
        while(s.source.volume < NewVolume)
        {
            s.source.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public void FadeIn(string name, float NewVolume, float FadeTime)
    {
        StartCoroutine(FadeInCor(name, NewVolume, FadeTime));
    }
    

    public IEnumerator FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float startVolume = s.source.volume;
        while(s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / 1f;
            yield return null;
        }
        s.source.Stop();
    }

    public void StopFades() {
        StopCoroutine("FadeOut");
        StopCoroutine("FadeInCor");
    }
}
