using XVNML.Core.Native;
using XVNML.Utilities.Macros;
using XVNML2U;

[MacroLibrary(typeof(InugamiMacroLibrary))]
public sealed class InugamiMacroLibrary : ActionSender
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
        GameManager.PlayerDataState.gameState.Set(flagValue);
    }

    [Macro("raise_game_state")]
    private static void RaiseGameStateMacro(MacroCallInfo info)
    {
        GameManager.PlayerDataState.gameState.Raise();
    }

    [Macro("save_game")]
    private static void SaveGameStateMacro(MacroCallInfo info)
    {
        GameManager.Save();
    }
}
