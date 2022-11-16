using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int playerHealth = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            ApplyDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void ApplyDamage(int damage)
    {
        playerHealth -= damage;
        audioPlayer.PlayExplosionClip();
        if(playerHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        if(!isPlayer)
        {
            scoreKeeper.AddScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
        
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetCurrentHealth()
    {
        return playerHealth;
    }
}
