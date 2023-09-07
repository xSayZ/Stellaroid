using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private enum DisplayCategory
    {
        Components,
        Settings,
    }

    private DisplayCategory categoryToDisplay;

    public override void OnInspectorGUI()
    {
        try
        {
            // Display the enum popup in the inspector
            categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);

            // Create a space to separate this enum popup from the other variables 
            EditorGUILayout.Space();

            switch (categoryToDisplay)
            {
                case DisplayCategory.Components:
                    DisplayComponents();
                    break;
                case DisplayCategory.Settings:
                    DisplaySettings();
                    break;
                default:
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
        catch (System.Exception e)
        {
            // Handle any exceptions that might occur during the GUI rendering
            Debug.LogError("An error occurred in the custom inspector: " + e.ToString());
        }

    }

    void DisplayComponents()
    {
        try
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ball"));
        }
        catch (System.Exception e)
        {
            // Handle any exceptions that might occur when displaying components
            Debug.LogError("An error occurred while displaying components: " + e.ToString());
        }
    }

    void DisplaySettings()
    {
        try
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("score"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("autoPlay"));
        }
        catch (System.Exception e)
        {
            // Handle any exceptions that might occur when displaying settings
            Debug.LogError("An error occurred while displaying settings: " + e.ToString());
        }
    }
}
