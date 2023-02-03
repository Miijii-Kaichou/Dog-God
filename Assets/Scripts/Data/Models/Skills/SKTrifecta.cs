using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKTrifecta : Skill, IEnhanceWithMado<MDPyromado>
{
    public override string SkillName => "Trifect";
    public override Type StaticItemType => typeof(SKTrifecta);
    public override ItemUseCallback OnActionUse => UseSkill;

    public MDPyromado MadoEnhancer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
