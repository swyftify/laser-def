using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float projectileSpeed = 10f;

    [SerializeField]
    float projectileTTL = 2f;

    [SerializeField]
    float baseFireRate = 0.2f;

    [Header("AI")]
    [SerializeField]
    public bool useAI;

    [SerializeField]
    float fireRateVariance = 0.15f;

    [SerializeField]
    float minFireRate = 0.1f;

    [HideInInspector]
    public bool isShooting;

    Coroutine shooting;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isShooting && shooting == null)
        {
            shooting = StartCoroutine(FireContinuously());
        }
        else if (!isShooting && shooting != null)
        {
            StopCoroutine(shooting);
            shooting = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectileInstance = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity
            );

            audioPlayer.PlayShootingClip();

            Rigidbody2D projectileRigidBody = projectileInstance.GetComponent<Rigidbody2D>();
            if (projectileRigidBody != null)
            {
                projectileRigidBody.velocity =
                    (useAI ? -transform.up : transform.up) * projectileSpeed;
            }

            Destroy(projectileInstance, projectileTTL);

            // Used to adjust fire rate for the enemy
            // fireRateVariance set to 0 for the player
            float adjustedFireRate = Random.Range(
                baseFireRate - fireRateVariance,
                baseFireRate + fireRateVariance
            );
            Mathf.Clamp(adjustedFireRate, minFireRate, float.MaxValue);

            yield return new WaitForSecondsRealtime(adjustedFireRate);
        }
    }
}
