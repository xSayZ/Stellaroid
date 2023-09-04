using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("Level Builder")]
    [Tooltip("Click to toggle if you want to load the level from a JSON file or from placed bricks")]
    [SerializeField]
    private bool JSONSelector = true;

    public TextAsset jsonFile;
    public BrickColorMapping colorMapping;
    bool levelLoaded = false;

    public bool LevelLoad()
    {
        return levelLoaded;
    }

    void Start()
    {
        if (JSONSelector)
        {
            LoadLevel(jsonFile);

        }
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

                    levelLoaded = true;
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