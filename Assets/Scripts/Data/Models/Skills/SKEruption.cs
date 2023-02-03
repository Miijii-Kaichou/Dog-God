using System;
using static SharedData.Constants;

/// <summary>
/// Blazing rocks falls from the sky.
/// Amplified with Pyromado equipped.
/// </summary>
public sealed class SKEruption : Skill, IEnhanceWithMado<MDPyromado>
{
    public override string SkillName => "Eruption";
    public override Type StaticItemType => typeof(SKEruption);
    public override ItemUseCallback OnActionUse => UseSkill;

    public MDPyromado MadoEnhancer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
