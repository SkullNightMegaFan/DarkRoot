using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip gunSound;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerStatus.OnPlayerDamaged += PlayDamageSound;  //adds function to OnPlayerDamagedEvent ?
        PlayerController.OnGunAttack += PlayGunFireSound;  //adds function to OnPlayerDamagedEvent ?
    }
    private void OnDisable()
    {
        PlayerStatus.OnPlayerDamaged -= PlayDamageSound;
        PlayerController.OnGunAttack -= PlayGunFireSound;
    }


    public void PlayGunFireSound()
    {
        audioSource.PlayOneShot(gunSound, 1f);
    }

    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSound, 1f);
    }


}
