using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static AudioManager instance;

    public float minTimeBetweenSounds = 0.7f;

    private bool isPlayingSound = false;
    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
}
