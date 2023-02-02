using System;
using System.Collections;
using System.Linq;
using static SharedData.Constants;

public class SkillSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, Type[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize] { One, One, One, One },
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    private int _skillRefCount;

    // If new skills needs to be added,
    // just initialize it here.
    public Skill[] SkillsList =
    {
        new SKBeelzebub(),
        new SKBlazeRunner(),
        new SKBless(),
        new SKBlizzard(),
        new SKBraceForImpact(),
        new SKCharmer(),
        new SKChatter(),
        new SKComfort(),
        new SKEmbrace(),
        new SKEngage(),
        new SKEruption(),
        new SKFury(),
        new SKGluttony(),
        new SKHeal(),
        new SKHolyFlare(),
        new SKHolyPrism(),
        new SKHolySmite(),
        new SKHolyThrust(),
        new SKInstinct(),
        new SKMeganddo(),
        new SKRailgun(),
        new SKSmash(),
        new SKTalonite(),
        new SKTrifecta(),
        new SKTyphoon(),
        new SKUltraheal(),
        new SKYeti()
    };

    private BitArray Accessibility;

    protected override void OnInit()
    {
        Accessibility = new BitArray(SkillsList.Length);

    }

    internal T GetSkill<T>() where T : Skill
    {
        return (T)ActionCategoryDetails.slots.Where(skill => skill.StaticItemType == typeof(T)).Single();
    }

    internal int GetRefCount()
    {
        return _skillRefCount;
    }

    internal void IncreaseRefCount()
    {
        _skillRefCount++;
    }

    internal void GainAccess(int index)
    {
        Accessibility[index] = true;
    }

    internal void Lock(int index)
    {
        Accessibility[index] = false;
    }
}
