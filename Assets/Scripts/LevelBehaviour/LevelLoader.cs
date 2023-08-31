using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public TextAsset jsonFile;
    public BrickColorMapping colorMapping;

    void Start()
    {
        LoadLevel(jsonFile);
    }

    void LoadLevel(TextAsset jsonAsset)
    {
        if (jsonAsset != null)
        {
            Debug.Log("Loading JSON file");
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonAsset.text);

            foreach (BrickData brickData in levelData.bricks)
            {
                Vector3 position = new Vector3(brickData.position.x, brickData.position.y, 0);
                GameObject brickPrefab = colorMapping.GetPrefabForColorIdentifier(brickData.colorIdentifier);

                if (brickPrefab != null)
                {
                    GameObject brickInstance = Instantiate(brickPrefab, position, Quaternion.identity);
                    Brick brickComponent = brickInstance.GetComponent<Brick>();

                    brickComponent.hitLimit = brickData.hitlimits;
                }
            }
        }
        else
        {
            Debug.LogError("JSON asset not assigned.");
        }
    }
}

[System.Serializable]
public class LevelData
{
    public BrickData[] bricks;
}

[System.Serializable]
public class BrickData
{
    public Vector2 position;
    public string colorIdentifier;
    public int hitlimits;
}


