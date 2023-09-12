using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private enum DisplayCategory
    {
        Settings,
        SoundSettings,
        Components,
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
                case DisplayCategory.Settings:
                    DisplaySettings();
                    break;
                case DisplayCategory.SoundSettings:
                    DisplaySoundSettings();
                    break;
                case DisplayCategory.Components:
                    DisplayComponents();
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

    void DisplaySoundSettings()
    {
        try
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundConfigurations"));
        }
        catch (System.Exception e)
        {
            // Handle any exceptions that might occur when displaying settings
            Debug.LogError("An error occurred while displaying settings: " + e.ToString());
        }
    }

    void DisplayComponents()
    {
        try
        {
            SerializedProperty pickups = serializedObject.FindProperty("balls");
            EditorGUILayout.PropertyField(pickups, true);

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
