using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Powerup : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5)]
    private float decendSpeed = 2f;

    [SerializeField]
    [Range(-1, 0)]
    private float accelerationSpeed = -0.25f;

    [SerializeField]
    [ReadOnly]
    private float currentSpeed;


    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = decendSpeed;
    }

    private string powerupName;

    public void Initialize(string name)
    {
        powerupName = name;
    }
    public string GetPowerupName()
    {
        return powerupName;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += accelerationSpeed * Time.deltaTime;

        if (currentSpeed < 0)
            currentSpeed = 0;

        transform.position += Vector3.down * decendSpeed * Time.deltaTime;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
