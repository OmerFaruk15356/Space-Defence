using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 15;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFireRate = 0.5f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFire;
    Coroutine firingCoroutine;
    AuidoPlayer auidoPlayer;
    private void Awake() 
    {
        auidoPlayer = FindObjectOfType<AuidoPlayer>();
    }
    void Start()
    {
        if(useAI)
        {
            isFire = true;
        }
    }

    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if(isFire && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        else if(!isFire && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    IEnumerator FireContinuosly()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
            Rigidbody2D rgb = instance.GetComponent<Rigidbody2D>();
            if(rgb != null)
            {
                rgb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance,projectileLifeTime);
            float timeToNextProjectile = Random.Range(baseFireRate - firingRateVariance, baseFireRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            auidoPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
 