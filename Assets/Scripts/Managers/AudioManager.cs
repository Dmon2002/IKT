using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bombExplosionSound;
    public AudioSource damageBuffSound;
    public AudioSource glassSound;
    public AudioSource hitSound;
    public AudioSource hpBonusSound;
    public AudioSource musicSound;
    public AudioSource playerDeadSound;
    public AudioSource stepSound;
    public AudioSource playerTakeDamage;

    public AudioSource selection;

    public AudioMixer mixer;

    public static AudioManager instance;

    public float minTimeBetweenSounds = 0.7f;

    private bool isPlayingSound = false;

    public bool MusicON
    {
        get
        {
            mixer.GetFloat("MusicVolume", out float currenValue);
            return currenValue == 0;
        }
        set
        {
            if (value)
            {
                mixer.SetFloat("MusicVolume", 0);
            }
            else
            {
                mixer.SetFloat("MusicVolume", -80);
            }
            
        }
    }

    public bool SoundsON
    {
        get
        {
            mixer.GetFloat("SoundsVolume", out float currenValue);
            return currenValue == 0;
        }
        set
        {
            if (value)
            {
                mixer.SetFloat("SoundsVolume", 0);
            }
            else
            {
                mixer.SetFloat("SoundsVolume", -80);
            }

        }
    }

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayBombExplosion()
    {
        bombExplosionSound.Play();
    }

    public void PlayDamageBuff()
    {
        damageBuffSound.Play();
    }

    public void PlayGlass()
    {
        glassSound.Play();
    }

    public void PlayHit()
    {
        hitSound.Play();
    }

    public void PlayHpBonus()
    {
        hpBonusSound.Play();
    }

    public void PlayMusic()
    {
        musicSound.Play();
    }

    public void PlayPlayerDead()
    {
        playerDeadSound.Play();
    }

    public void PlayStep()
    {
        //print("Plaaaay");
        stepSound.Play();
    }

    public void PlayPlayerTakeDamage()
    {
        playerTakeDamage.Play();
    }

    public void PlaySelection()
    {
        selection.PlayOneShot(selection.clip);
    }

    public void SwitchMusic()
    {
        if (MusicON)
        {
            MusicON = false;
        }
        else
        {
            MusicON= true;
        }
    }

    public void SwitchSounds()
    {
        if (SoundsON)
        {
            SoundsON = false;
        }
        else
        {
            SoundsON = true;
        }
    }
}
