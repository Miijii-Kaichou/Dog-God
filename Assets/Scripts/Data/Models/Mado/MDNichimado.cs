#nullable enable

using Extensions;

using static SharedData.Constants;

public sealed class MDNichimado : Mado, IMadoFusion
{
    // Need Main Component of 
    // Cryomado
    public MDPyromado? Pyromado { get; private set; }

    // Primary Component for a valid
    // Tsukimado is a
    // Hyromado
    public MDYamimado? JointMadoTrue { get; private set; }

    // Secondary Alternative Component for 
    // a valid Tsukimado is a
    // Yamimado
    public MDHyromado? JointMadoAlternative { get; private set; }

    // Essential Buffs
    // ...Goes Here...

    public void ValidateFusion(Mado[] additionalMado)
    {
        // We just need 2 mado for the fusion. Can't take
        // more than that.
        if (additionalMado.Length > 2) return;

        // Primary 
        var mainMado = additionalMado[Zero];

        // Validate if pairing mado is
        // Hyromado or Yamimado
        var pairingMado = additionalMado[One];

        // If our main mado is not Pyromado, invalid fusion
        if (mainMado.Is(typeof(MDPyromado)) == false) return;
        Pyromado = (MDPyromado)mainMado;

        // If our pairing mado isn't a Hyromado or Tsukimado
        // invalidate fusion
        if (mainMado.Is(typeof(MDYamimado)))
            JointMadoTrue = (MDYamimado)pairingMado;

        if (pairingMado.Is(typeof(MDHyromado)))
            JointMadoAlternative = (MDHyromado)pairingMado;
    }

    public bool IsAlternative =>
        JointMadoTrue == null &&
        JointMadoAlternative != null;
}