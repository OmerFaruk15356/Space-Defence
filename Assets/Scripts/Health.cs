using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healt = 50;
    [SerializeField] ParticleSystem damageEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    CameraShake cameraShake;
    AuidoPlayer auidoPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    private void Awake() 
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        auidoPlayer = FindObjectOfType<AuidoPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damage damage = other.GetComponent<Damage>();
        if(damage != null)
        {
            TakeDamage(damage.GetDamage());
            PlayerDamageEffect();
            auidoPlayer.PlayDamageClip();
            damage.Hit();
            ShakeCamera();
        }
    }
    void TakeDamage(int damage)
    {
        healt -= damage;
        if(healt <= 0)
        {
            Die();
        }
    }
    void PlayerDamageEffect()
    {
        if(damageEffect != null)
        {
            ParticleSystem instance = Instantiate(damageEffect, transform.position, Quaternion.identity);
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
    public int GetHealt()
    {
        return healt;
    }
    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else{
            levelManager.LoadGameOverMenu();
        }
        Destroy(gameObject);
    }
}
