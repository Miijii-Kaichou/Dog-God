using System;

using static SharedData.ReadOnlyData;

/// <summary>
/// This is a Holy (Light)-Based Mado. Can be paired with the following skills:
/// Skill: Fury(becomes Typhoon)
///Skill: Holy Smite
/// Skill: Holy Thrust
/// Skill: Holy Flare
/// Skill: Railgun
/// Skill: Holy Prism
/// Rosa's Divine Skill: "Shower Me in a Thousand Rose Petals"
/// Othella's Divine Skill: Kagami
/// Patchouli's Divine Skill: Catastrophic Calamity Stream
/// Ryuga's Divine Blessing: Sterling
/// ----
/// Normal Attacks increases by 50%
/// All Skills Listed is Enhanced by 50%
/// Defense increases by 30%
/// Item Effectiveness Doubles
/// </summary>
public sealed class MDHyromado : Mado, ISkillEffectiveness, IDefenseModifier, IItemEffectiveness
{
    public override string MadoName => "Hyromado";
    public override Type StaticItemType => typeof(MDHyromado);
    public override ItemUseCallaback OnActionUse => Infuse;

    public TargetInfo<Skill>[] TargetSkills => new TargetInfo<Skill>[6]
    {
        TargetInfo<SKTyphoon>   .Target(),
        TargetInfo<SKHolySmite> .Target(),
        TargetInfo<SKHolyThrust>.Target(),
        TargetInfo<SKHolyFlare> .Target(),
        TargetInfo<SKRailgun>   .Target(),
        TargetInfo<SKHolyPrism> .Target()
    };
    public float SetDefenseBonus => 30f;

    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;

    public TargetInfo<Item>[] TargetItems => TargetAllItems;

    private void Infuse()
    {
        throw new NotImplementedException();
    }
}
