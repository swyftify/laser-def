using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int health = 50;

    [SerializeField]
    ParticleSystem explosionEffect;

    CameraShake cameraShake;

    [SerializeField]
    bool applyCameraShake,
        isPlayer;

    [SerializeField]
    int enemyScoreValue;

    AudioPlayer audioPlayer;

    ScoreKeeper scoreKeeper;

    LevelManager levelManager;

    //On collision with a damage dealer the item takes damage.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Enemy is a damage dealer and has a damage value
        DamageDealer damageDealer = collider.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            // Damage delaer gets destoyed on collision
            damageDealer.Hit();
        }
    }

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void ShakeCamera()
    {
        if (applyCameraShake && cameraShake != null)
        {
            cameraShake.Play();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        audioPlayer.PlayDamageClip();
        if (health <= 0)
        {
            if (!isPlayer)
            {
                scoreKeeper.AddToScore(enemyScoreValue);
            }
            else if (isPlayer)
            {
                levelManager.LoadGameOver();
                scoreKeeper.ResetScore();
            }

            audioPlayer.PlayExplosionClip();
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if (explosionEffect != null)
        {
            ParticleSystem instance = Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
            // Destroy the particle system after duration + lifetime of the particles
            Destroy(
                instance.gameObject,
                instance.main.duration + instance.main.startLifetime.constantMax
            );
        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return this.health;
    }
}
