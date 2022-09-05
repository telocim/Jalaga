using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = .2f;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFireRate = 0.1f;
    [SerializeField] bool useAI;

    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            float timetoNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timetoNextProjectile = Mathf.Clamp(timetoNextProjectile, minimumFireRate, float.MaxValue);
            Debug.Log(timetoNextProjectile);

            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(timetoNextProjectile);
        }
    }
}
