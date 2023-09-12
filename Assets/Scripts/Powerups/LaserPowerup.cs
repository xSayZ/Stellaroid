using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerup : MonoBehaviour
{
    [Tooltip("Times shooting occurs per seconds")]
    [SerializeField]
    [Range(0, 5)]
    private float fireRatePerSeconds = 3;

    [Tooltip("Amount of times bullets are shot")]
    [SerializeField]
    [Range(0, 20)]
    private int amountToShoot = 3;

    public float ReturnFireRate()
    {
        float fireRate = fireRatePerSeconds * (Time.deltaTime * 20);

        return fireRate;
    }

    public int ReturnAmountToShoot()
    {
        return amountToShoot;
    }
}

