using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ResetPlayerPrefsEditor
{

    static ResetPlayerPrefsEditor()
    {
        // When Unity changes its playmode
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // If you exit playmode, reset all scores
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }

}
