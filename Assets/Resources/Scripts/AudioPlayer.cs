using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Song;

    public void Play()
    {
        Song.Play();
    }

    public void Stop()
    {
        Song.Stop();
    }
}
