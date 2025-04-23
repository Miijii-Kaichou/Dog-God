using XVNML.Core.Native;
using XVNML.Utilities.Dialogue;
using XVNML.Utilities.Macros;
using XVNML2U;
using XVNML2U.Data;
using XVNML2U.Mono;

[MacroLibrary(typeof(InugamiMacroLibrary))]
public sealed class InugamiMacroLibrary : ActionSender<InugamiMacroLibrary>
{
    [Macro("enable_input")]
    private static void EnableInputMacro(MacroCallInfo info, int inputLength)
    {
        throw new System.NotImplementedException();
    }

    [Macro("enable_prompt")]
    private static void EnablePrompt(MacroCallInfo info) 
    {
        throw new System.NotImplementedException();
    }

    [Macro("do_fade_screen_to_white")]
    private static void FadeScreenToWhiteMacro(MacroCallInfo info)
    {
        var tweenObj = (RuntimeReferenceTable.Get("FadeToWhiteTween").value as CanvasGroupFadeInTween);
        tweenObj.DoFadeInTweening();
    }

    [Macro("set_game_state")]
    private static void SetGameStateMacro(MacroCallInfo info, int flagValue)
    {
        UnityEngine.Debug.Log($"Set game state: {flagValue}");

        var gameData = GameManager.PlayerDataState;
        if (gameData == null)
        {
            SaveGameStateMacro(info);
            GameManager.Load();
            gameData = GameManager.PlayerDataState;
        }

        gameData.gameState.Set(flagValue);
    }

    [Macro("raise_game_state")]
    private static void RaiseGameStateMacro(MacroCallInfo info)
    {
        var gameData = GameManager.PlayerDataState;
        if (gameData == null) SaveGameStateMacro(info);
        gameData.gameState.Raise();
    }

    [Macro("save_game")]
    private static void SaveGameStateMacro(MacroCallInfo info)
    {
        GameManager.Save();
    }

    [Macro("scene_index")]
    [Macro("sidx")]
    private static void LoadSceneByIndex(MacroCallInfo info, int index)
    {
        GameSceneManager.Prepare(index);
        GameSceneManager.Deploy();
        UnityEngine.Debug.Log($"Scene {index} loaded...");
    }

    [Macro("disable_user_input")]
    [Macro("duin")]
    private static void DisableUserInputMacro(MacroCallInfo info)
    {
        XVNMLInputManager.Enabled = false;
    }

    [Macro("shop_text_speed")]
    private static void ShopTextSpeed(MacroCallInfo info)
    {
        
    }
}
