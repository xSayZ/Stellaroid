using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField]
    private SoundData soundConfiguration;

    [SerializeField]
    private float projectileSpeed = 2f;

    [SerializeField]
    private GameObject projectilePrefab;


    public void Shoot()
    {
        // Play sound
        EventManager.OnPlaySound.Invoke(soundConfiguration);

        // Create a new bullet instance
        GameObject instantiatedProjectile = Instantiate(projectilePrefab);

        Projectile projectileInstance = instantiatedProjectile.GetComponent<Projectile>();
        
        // Set the initial position of the bullet to match the player's position
        projectileInstance.transform.position = gameObject.transform.position;

        if (projectileInstance != null)
        {
            projectileInstance.Initialize(projectileSpeed);
        }
    }

}
