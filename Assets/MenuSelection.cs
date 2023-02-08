using UnityEditor;
using UnityEngine;

using static SharedData.Constants;

public class MenuSelection : MonoBehaviour
{
    public void OnStartSelect()
    {
        // View Player Profile List
        GameSceneManager.LoadScene(SI_Profile, extendNavigation: true);
    }

    public void OnContinueSelect()
    {
        // Continue from last accessed player data.
        // Give feedback to player if you can't continue
        // (because the character you played can't resurrect)
    }

    public void OnSettingsSelect()
    {
        // View Settings Menu
    }

    public void OnQuitSelect()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
            return;
        }
#endif //UNITY_EDITOR

        Application.Quit();
    }
}
