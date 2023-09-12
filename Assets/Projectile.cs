using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projectileSpeed;

    // Gets the projectileSpeed from 'LaserWeapon.cs' that instantiates the object
    public void Initialize(float speed)
    {
        projectileSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the gameObject upwards
        transform.position += Vector3.up * projectileSpeed * Time.deltaTime;

        // Destroys the game object if no camera can see the renderer
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}
