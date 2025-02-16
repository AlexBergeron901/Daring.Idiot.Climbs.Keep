using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectMixerGroup;
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.loop = s.isLoop;


            switch (s.audioType) 
            {

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectMixerGroup;
                    break;
            }



            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    public void Play(string clipname) 
    {
        Sound s = System.Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + clipname + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipname)
    {
        Sound s = System.Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + clipname + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void UpdateMixerVolume()
    {
        {
            musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume)*20);
            soundEffectMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
        }
    }


    public void SetGameVolume(float volume)
    {
        // update volume again when slider changes
        AudioListener.volume = volume;

        // this SAVES the incoming slider change for next time
        PlayerPrefs.SetFloat("volume", volume);


    }

}
