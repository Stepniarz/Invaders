using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 0.1f;

    [HideInInspector] public bool isShooting;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if(useAI)
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
        if(isShooting && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireCountinuosly());
        }
        else if(!isShooting && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireCountinuosly()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
