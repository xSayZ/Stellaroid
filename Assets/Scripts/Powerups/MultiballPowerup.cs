using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiballPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private int amountToSpawn;

    public void OnPickUp(Vector3 spawnPos)
    {
        spawnBalls(spawnPos);
    }

    public void spawnBalls(Vector3 spawnPos)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            var ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            var randomIndex = Random.Range(-3, 3);
            var randomVectorUpwards = new Vector3(randomIndex, 1, 0);
            ball.GetComponent<Ball>().Serve(randomVectorUpwards);
        }
    }
}
