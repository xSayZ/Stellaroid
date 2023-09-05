using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("Level Builder")]
    [Tooltip("Click to toggle if you want to load the level from a JSON file or from placed bricks.")]
    [SerializeField]
    private bool JSONSelector = true; // Use JSON file by default

    [Tooltip("The JSON file containing level data.")]
    [SerializeField]
    private TextAsset[] jsonFile;

    [Tooltip("The color mapping for bricks.")]
    [SerializeField]
    private BrickColorMapping colorMapping;

    private bool levelLoaded = false; // Indicates whether the level has been loaded

    // Indicates whether the level has been loaded
    public bool LevelLoad()
    {
        return levelLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Check if JSONSelector is true (loading from JSON file)
        if (JSONSelector)
        {
            // Load the level from the specified JSON file
            LoadLevel(jsonFile[0]);
        }
    }

    // Method to load a level from a JSON asset
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

                    brickComponent.HitLimit = brickData.hitlimits;

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