using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField]
    AudioClip shootingClip;

    [SerializeField]
    [Range(0f, 1f)]
    float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField]
    AudioClip damageClip;

    [SerializeField]
    [Range(0f, 1f)]
    float damageVolume = 1f;

    [Header("Explosion")]
    [SerializeField]
    AudioClip explosionClip;

    [SerializeField]
    [Range(0f, 1f)]
    float explosionVolume = 1f;

    // Available between all instances of the class
    // Either be public or create a getter and setter
    static AudioPlayer instance;

    private void Awake()
    {
        ManageSingleton();
    }

    //Singleton Pattern
    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // void ManageSingleton()
    // {
    //     int instanceCount = FindObjectsOfType(GetType()).Length;
    //     if (instanceCount > 1)
    //     {
    //         gameObject.SetActive(false);
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    public void PlayShootingClip()
    {
        this.PlayAudioClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        this.PlayAudioClip(damageClip, damageVolume);
    }

    public void PlayExplosionClip()
    {
        this.PlayAudioClip(explosionClip, explosionVolume);
    }

    private void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}
