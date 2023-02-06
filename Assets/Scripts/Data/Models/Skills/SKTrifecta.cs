using System;
using UnityEngine;

using static SharedData.Constants;

/// <summary>
/// A 3-hit combo move that forms a triangle.
/// Amplified if Pyromado is equipped
/// </summary>
public sealed class SKTrifecta : Skill, IEnhanceWithMado<MDPyromado>
{
    public override string SkillName => "Trifecta";
    public override Type StaticItemType => typeof(SKTrifecta);
    public override ItemUseCallback OnActionUse => UseSkill;

    public MDPyromado MadoEnhancer { get; set; }

    private void UseSkill()
    {
        MadoSystem.FindTraitsOf(MadoEnhancer);
        Debug.Log($"Used {SkillName}");
    }
}
