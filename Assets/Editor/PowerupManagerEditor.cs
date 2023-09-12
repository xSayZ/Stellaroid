using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PowerupManager))]
public class PowerupManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PowerupManager manager = (PowerupManager)target;

        // Display baseDropChance slider with a label.
        manager.baseDropChance = EditorGUILayout.Slider("Base Drop Chance", manager.baseDropChance, 0f, 1f);

        // Display chanceIncreasePerDestroy slider with a label.
        manager.chanceIncreasePerDestroy = EditorGUILayout.Slider("Chance Increase per Destroy", manager.chanceIncreasePerDestroy, 0.001f, 0.2f);

        EditorGUILayout.Space(); // Add some spacing.

        EditorGUILayout.LabelField("Current Odds", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Current Drop Chance: " + manager.currentDropChance.ToString("0.00"));
        EditorGUILayout.LabelField("Bricks Destroyed: " + manager.bricksDestroyed);

        EditorGUILayout.Space(); // Add more spacing.

        // Display a button to reset the odds.
        if (GUILayout.Button("Reset Odds"))
        {
            manager.ResetOdds();
        }

        EditorGUILayout.Space(); // Add some final spacing.

        // Display the 'pickups' array.
        SerializedProperty powerups = serializedObject.FindProperty("powerups");
        EditorGUILayout.PropertyField(powerups, true);

        // Apply changes to the serialized object.
        serializedObject.ApplyModifiedProperties();
    }
}
