using Extensions;
using System.Collections;
using System.Collections.Generic;
#nullable enable

using static SharedData.Constants;

public sealed class MDTsukimado : Mado, IMadoFusion
{
    // Need Main Component of 
    // Cryomado
    public MDCryomado? Cryomado { get; private set; }

    // Primary Component for a valid
    // Tsukimado is a
    // Hyromado
    public MDHyromado? JointMadoTrue { get; private set; }
    
    // Secondary Alternative Component for 
    // a valid Tsukimado is a
    // Yamimado
    public MDYamimado? JointMadoAlternative { get; private set; }

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

        // If our main mado is not Cryomado, invalid fusion
        if (mainMado.Is(typeof(MDCryomado)) == false) return;
        Cryomado = (MDCryomado)mainMado;

        // If our pairing mado isn't a Hyromado or Tsukimado
        // invalidate fusion
        if (pairingMado.Is(typeof(MDHyromado)))
            JointMadoTrue = (MDHyromado)pairingMado;

        if (mainMado.Is(typeof(MDYamimado)))
            JointMadoAlternative = (MDYamimado)pairingMado;
    }

    public bool IsAlternative => 
        JointMadoTrue == null && 
        JointMadoAlternative != null;
}
