using UnityEngine;

[CreateAssetMenu(fileName = "BrickColorMapping", menuName = "Custom/Brick Color Mapping")]
public class BrickColorMapping : ScriptableObject
{
    [System.Serializable]
    public class ColorPrefabPair
    {
        public string colorIdentifier;
        public GameObject prefab;
    }

    public ColorPrefabPair[] colorPrefabs;

    public GameObject GetPrefabForColorIdentifier(string colorIdentifier)
    {
        foreach (var pair in colorPrefabs)
        {
            if (pair.colorIdentifier == colorIdentifier)
            {
                return pair.prefab;
            }
        }
        Debug.LogWarning("No prefab found for color identifier: " + colorIdentifier);
        return null;
    }

    public string GetColorIdentifier(int index)
    {
        return colorPrefabs[index].colorIdentifier;
    }
}
