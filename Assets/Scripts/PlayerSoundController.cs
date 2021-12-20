using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum Sound
//{
//    move
//}

public class PlayerSoundController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip moveSound;
    public AudioClip deathSound;
    public AudioClip teleportSound;
    public AudioClip flagGet;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMoveSound ()
    {
        audioSource.PlayOneShot(moveSound);
    }

    public void PlayDeathSound ()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayTeleportSound()
    {
        audioSource.PlayOneShot(teleportSound);
    }

    public void PlayFlagGetSound()
    {
        audioSource.PlayOneShot(flagGet);
    }
}
