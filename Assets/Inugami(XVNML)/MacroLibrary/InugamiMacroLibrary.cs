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
    private static void FadeScreenToWhiteMacro(MacroCallInfo info, float duration)
    {
        throw new System.NotImplementedException();
    }
}
