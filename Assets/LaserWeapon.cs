using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{

    [SerializeField]
    private float projectileSpeed = 1f;

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectileInstance;


    public void Shoot()
    {
        // Create a new bullet instance
        projectileInstance = Instantiate(projectilePrefab);

        // Set the initial position of the bullet to match the player's position
        projectileInstance.transform.position = gameObject.transform.position;
    }


    private void Update()
    {

        if(projectileInstance != null)
        {
            projectileInstance.transform.position += Vector3.up * projectileSpeed * Time.deltaTime;
        }
    }
}
