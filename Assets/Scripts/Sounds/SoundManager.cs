using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public class SoundSetup
    {
        public string Name;
        public AudioSource Audio;
        public bool Solo;
    }

    private Dictionary<string, SoundSetup> soundsMap = new();
    private Dictionary<string, int> soundsBeingPlayed = new();

    private void OnDisable()
    {
        UnregisterAllSounds();
    }

    public bool RegisterSound(SoundSetup audioSetup)
    {
        if (soundsMap.ContainsKey(audioSetup.Name))
        {
            return false;
        }

        soundsMap.Add(audioSetup.Name, audioSetup);
        if (audioSetup.Audio.playOnAwake)
        {
            soundsBeingPlayed.Add(audioSetup.Name, 1);
        }
        return true;
    }

    private void UnregisterAllSounds()
    {
        soundsMap.Clear();
    }

    public AudioSource GetSound(string name)
    {
        return soundsMap[name].Audio;
    }

    public void PlaySound(string name, bool loop = false)
    {
        if (!soundsMap.ContainsKey(name))
        {
            throw new Exception("The sound effect " + name + " has not been registered");
        }

        if(soundsBeingPlayed.ContainsKey(name) && soundsMap[name].Solo)
        {
            return;
        }

        if(!soundsBeingPlayed.ContainsKey(name))
        {
            soundsBeingPlayed.Add(name, 0);
        }

        soundsBeingPlayed[name]++;
        if (soundsMap[name].Audio.loop)
        {
            soundsMap[name].Audio.Play();
        }
        else
        {
            soundsMap[name].Audio.PlayOneShot(soundsMap[name].Audio.clip);
            StartCoroutine(OnStop(name, soundsMap[name].Audio.clip.length));
        }
        
    }

    public void StopSound(string name)
    {
        if (!soundsMap.ContainsKey(name))
        {
            throw new Exception("The sound effect " + name + " has not been registered");
        }

        if (!soundsBeingPlayed.ContainsKey(name))
        {
            return;
        }

        soundsMap[name].Audio.Stop();
        soundsBeingPlayed[name]--;
        if (soundsBeingPlayed[name] == 0)
        {
            soundsBeingPlayed.Remove(name);
        }
    }

    private IEnumerator OnStop(string name, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (soundsBeingPlayed.ContainsKey(name))
        {    
            soundsBeingPlayed[name]--;
            if (soundsBeingPlayed[name] == 0)
            {
                soundsBeingPlayed.Remove(name);
            }
        }
    }

}
