using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    public float baseDropChance = 0.2f;
    [SerializeField]
    [Range(0.001f,0.2f)]
    public float chanceIncreasePerDestroy = 0.01f;

    public float currentDropChance;
    public int bricksDestroyed;

    public PowerupData[] pickups;

    public void DropRandomPickup(Vector2 spawnPosition)
    {
        bricksDestroyed++;
        if (pickups.Length > 0)
        {
            // Increase currentDropChance based on bricksDestroyed if needed.
            currentDropChance += bricksDestroyed * chanceIncreasePerDestroy;

            // Ensure the drop chance doesn't exceed 1.0 (100% chance).
            currentDropChance = Mathf.Clamp01(currentDropChance);

            if(Random.value <= currentDropChance)
            {
                Debug.Log("Powerup Drop");
                int randomIndex = Random.Range(0, pickups.Length);
                GameObject selectedPickupPrefab = pickups[randomIndex].powerupPrefab;

                ResetOdds();

                if (selectedPickupPrefab != null)
                {
                    Instantiate(selectedPickupPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    public void ResetOdds()
    {
        currentDropChance = baseDropChance;
        bricksDestroyed = 0;
    }

}

[System.Serializable]
public class PowerupData
{
    public string powerupName;
    public GameObject powerupPrefab;
}
