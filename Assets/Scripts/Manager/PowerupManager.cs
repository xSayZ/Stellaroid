using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public PickupData[] pickups;

    private float baseDropChance = 0.2f;
    private float chanceIncreasePerDestroy;

    public void DropRandomPickup(Vector2 spawnPosition)
    {
        if (pickups.Length > 0)
        {
            float currentDropChance = baseDropChance;

            int bricksDestroyed = BrickManager.Instance.BricksDestroyed();
            currentDropChance += bricksDestroyed * chanceIncreasePerDestroy;

            currentDropChance = Mathf.Clamp01(currentDropChance);

            if(Random.value <= currentDropChance)
            {
                Debug.Log("Powerup Drop");
                int randomIndex = Random.Range(0, pickups.Length);
                GameObject selectedPickupPrefab = pickups[randomIndex].powerupPrefab;

                if(selectedPickupPrefab != null)
                {
                    Instantiate(selectedPickupPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

}

[System.Serializable]
public class PickupData
{
    public string powerupName;
    public GameObject powerupPrefab;
}
