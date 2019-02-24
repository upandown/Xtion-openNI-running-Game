using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

  public static AudioManager Instance;

  public AudioSource BGMPlayer;

  public AudioSource SoundPlayer;

  void Start()
  {
    Instance = this;
  }
  public void PlayBGM(string name)
  {
    if (BGMPlayer.isPlaying == false)
    {
      AudioClip clip = Resources.Load<AudioClip>(name);
      BGMPlayer.clip = clip;
      BGMPlayer.Play();
    }
  }
  public void StopBGM()
  {
    BGMPlayer.Stop();
  }
  public void PlaySound(string name)
  {
    AudioClip clip = Resources.Load<AudioClip>(name);
    SoundPlayer.PlayOneShot(clip);
  }
}