using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerup : MonoBehaviour
{

    [SerializeField]
    private float projectileSpeed = 1f;

    [SerializeField]
    private float fireRate;


    void OnPickup()
    {
        Destroy(gameObject);
    }

}

