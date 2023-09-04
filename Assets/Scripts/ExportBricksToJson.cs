using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ExportBricksToJson : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Toggle to export current level to JSON File")]
    private bool exportLevel;

    [SerializeField]
    private string exportFileName = "levelData.json"; // Change this to your desired file name.
    
    [SerializeField]
    private BrickColorMapping brickColorMapping;

    private void Start()
    {
        if (exportLevel)
        {
            // Find all objects with the "Brick.cs" script attached.
            Brick[] bricks = FindObjectsOfType<Brick>();

            // Create an array to store the brick data.
            BrickData[] brickDataArray = new BrickData[bricks.Length];

            // Fill the brick data array with information from each Brick component.
            for (int i = 0; i < bricks.Length; i++)
            {
                Brick brick = bricks[i];
                BrickData brickData = new BrickData();
                brickData.position = new Vector2(brick.transform.position.x, brick.transform.position.y);
                brickData.colorIdentifier = brickColorMapping.GetColorIdentifier(i);
                brickData.hitlimits = brick.GetHitLimits();

                brickDataArray[i] = brickData;
            }

            // Create a LevelData instance and assign the brick data array.
            LevelData levelData = new LevelData();
            levelData.bricks = brickDataArray;

            // Serialize the LevelData instance to JSON.
            string json = JsonUtility.ToJson(levelData);

            // Specify the path where you want to save the JSON file.
            string path = Path.Combine(Application.dataPath, exportFileName);

            // Write the JSON data to the file.
            File.WriteAllText(path, json);

            Debug.Log("Brick data exported to " + path);
        }
    }
}
