using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SharedData.Constants;
using static Extensions.Extensions;

public static class GameSceneManager
{
    public static int? StagePrepped { get; private set; } = null;
    public static UnityEngine.Events.UnityAction<Scene, LoadSceneMode> OnSceneLoaded;

    public static Navigation<int> SceneNavigation = new();

    public static Action OnSceneNavigationUpdate;


    static int PreviousScene;
    static int ActiveScene;
    public static LoadSceneMode LoadingMode = LoadSceneMode.Single;
    public static bool LoadingComplete = true;
    public static bool Initialized = false;
    public static IEnumerator loadCycle;

    static Dictionary<int, State<int>> stateDictonary = new();

    public static void Start() => LoadScene(SI_TitleScreen);

    public static void PublishStateValue(int publishingState)
    {
        try
        {
            stateDictonary.Add(ActiveScene, State<int>.New(publishingState));
        }
        catch
        {
            //Update the state of the entry that already exists
            stateDictonary[ActiveScene].value = publishingState;
        }
    }

    public static int GetState()
    {
        if (stateDictonary.ContainsKey(ActiveScene) == false) return 0;
        return stateDictonary[ActiveScene].value;
    }

    public static void LoadScene(int buildIndex, bool extendNavigation = false, bool resetNavigationTree = false)
    {
        buildIndex += 1;

        if (resetNavigationTree)
        {
            ActiveScene = buildIndex;
            SceneNavigation = new Navigation<int>();
        }


        Extensions.Coroutine.Start(LoadSceneAsync(buildIndex));

        if (!extendNavigation) return;
        SceneNavigation.Stretch(ActiveScene);
    }

#nullable enable
    static IEnumerator Unload()
    {
        AsyncOperation? ao1 = new();

        if (PreviousScene == -1)
        {
            PreviousScene = ActiveScene;
            yield break;
        }

        if (SceneManager.GetActiveScene().buildIndex == PreviousScene)
            ao1 = SceneManager.UnloadSceneAsync(PreviousScene);

        if (ao1 == null)
            yield break;

        yield return ao1;

        PreviousScene = ActiveScene;
    }
#nullable disable
    static IEnumerator LoadSceneAsync(int buildIndex)
    {
        AsyncOperation ao1 = new();

        SceneManager.LoadSceneAsync(buildIndex, LoadingMode);
        yield return ao1;

        ActiveScene = buildIndex;

        OnSceneLoaded?.Invoke(SceneManager.GetSceneByBuildIndex(buildIndex), LoadingMode);

        Extensions.Coroutine.Start(Unload());
    }

    public static void LoadPrevious(int distance = 1)
    {
        var targetScene = SceneNavigation.Condense(distance);
        Extensions.Coroutine.Start(LoadSceneAsync(targetScene));
        ActiveScene = targetScene;
    }

    public static void ChangeLoadingMode(LoadSceneMode loadingMode) => LoadingMode = loadingMode;

    /// <summary>
    /// Get a scene ready to load before hand
    /// </summary>
    /// <param name="distance"></param>
    internal static void Prepare(int index) => StagePrepped = index;

    /// <summary>
    /// To load a scene that you've prepared.
    /// </summary>
    internal static void Deploy() => LoadScene(StagePrepped ?? SI_TitleScreen);

}
