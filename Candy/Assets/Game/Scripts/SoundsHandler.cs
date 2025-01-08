using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsHandler : MonoBehaviour
{
    public static SoundsHandler sound;

    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _soundsMusic;

    [Header("Sounds")]
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _boomb;
    [SerializeField] private AudioClip _success;
    [SerializeField] private AudioClip _false;

    private bool isSound = true;
    private bool isMusic = true;

    public enum NameSoundGame
    {
        Click,
        Boom,
        Success,
        False
    }

    private void Awake()
    {
        if (sound == null) sound = this;
    }

    public bool ActiveSounds()
    {
        isSound = !isSound;

        return isSound;
    }

    public bool ActiveMusic()
    {
        isMusic = !isMusic;

        if (isMusic) { _backgroundMusic.Play();
            Debug.Log("Play");
        }
        else _backgroundMusic.Stop();
        return isMusic;
    }

    public void PlayShotSound(NameSoundGame nameSoundG)
    {
        if (isSound)
        {
            switch (nameSoundG)
            {
                case NameSoundGame.Click:
                    _soundsMusic.PlayOneShot(_click);
                    break;
                case NameSoundGame.Boom:
                    _soundsMusic.PlayOneShot(_boomb);
                    break;
                case NameSoundGame.Success:
                    _soundsMusic.PlayOneShot(_success);
                    break;
                case NameSoundGame.False:
                    _soundsMusic.PlayOneShot(_false);
                    break;
            }
        }
    }
}
