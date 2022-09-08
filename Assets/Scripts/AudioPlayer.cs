using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = .4f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = .4f;

    public void PlayShootingClip()
    {
            playClip(shootingClip, shootingVolume);        
    }
    public void PlayDamageClip()
    {
        playClip(damageClip, damageVolume);

    }

    void playClip(AudioClip clip, float volume)
    {
        if (clip!=null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

}
