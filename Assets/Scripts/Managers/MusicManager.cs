using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static SharedData.Constants;

public sealed class MusicManager : MonoBehaviour
{
    /*Music Manager will essentially collect all music files
     * from all SongEntities detected by RoftScouter,
     * and it will preview it for the player.
     * 
     * Music Manager will later be changed to
     * SongEntityManager, because instead of just Music clips,
     * it'll be a class that holds not only the song (in which it'll go through and play),
     * but give us all kinds of different information.
     */

    private static MusicManager Instance;

    [System.Serializable]
    public class Music
    {
        public string name; // Name of the audio

        public AudioClip clip; //The Audio Clip Reference

        [Range(ZERO, ONE)]
        public float volume; //Adjust Volume

        [Range(.1f, 3f)]
        public float pitch; //Adject pitch

        public bool enableLoop; //If the audio can repeat

        [HideInInspector] public AudioSource source;
    }

    public AudioMixerGroup musicMixer;

    public Slider musicVolumeAdjust; //Reference to our volume sliders

    public Music[] getMusic;

    public Music nowPlaying;
    public static Music NowPlaying;
    public static AudioSource NowPlayingSource;

    // Start is called before the first frame update
    public float timeSamples;

    public float[] positionSeconds;

    public static int GetSamples() => NowPlaying.source.timeSamples;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        foreach (Music a in getMusic)
        {
            a.source = gameObject.AddComponent<AudioSource>();

            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.enableLoop;
            a.source.outputAudioMixerGroup = musicMixer;
        }
    }


    /// <summary>
    /// Play audio and adjust its volume.
    /// </summary>
    /// 
    /// <param name="_name"></param>
    /// The audio clip by name.
    /// 
    /// <param name="_volume"></param>
    /// Support values between 0 and 100.
    ///
    public static Music Play(string _name, float _volume = HUNDRED, bool _oneShot = false)
    {
        if (NowPlaying != null && _name == NowPlaying.name)
        {
            if (!NowPlayingSource.isPlaying)
                NowPlayingSource.Play();
            return NowPlaying;
        }

        Music a = Array.Find(Instance.getMusic, sound => sound.name == _name);

        if (a == null)
        {
            Debug.LogWarning("Sound name " + _name + " was not found.");
            return null;
        }

        //Turn off previously playing music
        if (NowPlaying != null)
            StopNowPlaying();

        NowPlaying = a;
        NowPlayingSource = a.source;

        Instance.nowPlaying = NowPlaying;

        switch (_oneShot)
        {
            case true:
                NowPlayingSource.PlayOneShot(a.clip, _volume / HUNDRED);
                break;
            default:
                NowPlayingSource.Play();
                NowPlayingSource.volume = _volume / HUNDRED;
                break;

        }

        return NowPlaying;
    }

    public static void Stop(string _name)
    {
        Music a = Array.Find(Instance.getMusic, sound => sound.name == _name);
        if (a == null)
        {
            Debug.LogWarning("Music name " + _name + " was not found.");
            return;
        }

        a.source.Stop();
        NowPlaying = null;
        Instance.nowPlaying = NowPlaying;
    }

    public static void Pause(string _name)
    {
        Music a = Array.Find(Instance.getMusic, sound => sound.name == _name);
        if (a == null)
        {
            Debug.LogWarning("Music name " + _name + " was not found.");
            return;
        }

        a.source.Pause();
    }

    internal static Music FindMusicByIndex(int musicIndex)
    {
        Music a = Instance.getMusic[musicIndex];
        return a;
    }

    internal static Music FindMusic(string name)
    {
        Music a = Array.Find(Instance.getMusic, sound => sound.name == name);
        return a;
    }

    public static bool Exists(string _name)
    {
        Music a = Array.Find(Instance.getMusic, sound => sound.name == _name);
        return a == null ? false : true;
    }

    public static void SetVolume(string _name, float _value)
    {
        Music a = Array.Find(Instance.getMusic, sound => sound.name == _name);
        if (a == null)
        {
            Debug.LogWarning("Music name " + _name + " was not found.");
            return;
        }
        a.source.volume = _value;
    }

    public static void StopNowPlaying()
    {
        if (NowPlaying == null) return;
        Stop(NowPlaying.name);
    }

    public static void PauseNowPlaying()
    {
        if (NowPlaying == null) return;
        Pause(NowPlaying.name);
    }

    public static void ResumeNowPlaying()
    {
        if (NowPlaying == null) return;
        Play(NowPlaying.name);
    }

    public static void StopNowPlayingFade()
    {
        Instance.StartCoroutine(StopNowPlayingFadeCycle());
    }
    static IEnumerator StopNowPlayingFadeCycle()
    {
        while (NowPlaying != null && NowPlaying.source.isPlaying)
        {

            NowPlayingSource.volume -= 0.005f;
            if (NowPlayingSource.volume <= 0.1f)
                StopNowPlaying();

            yield return null;
        }
    }
}
