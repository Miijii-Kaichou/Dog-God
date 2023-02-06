#nullable enable

using System;
using System.Collections;
using System.Linq;
using static SharedData.Constants;

public class SkillSystem : GameSystem, IActionCategory
{
    private static SkillSystem? Self;

    public (IActionableItem?[] slots, int[] quantities, int[] capacities, Type?[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize] { One, One, One, One },
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    private static int _SkillRefCount;

    // If new skills needs to be added,
    // just initialize it here.
    public readonly static Skill[] SkillsList =
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
        new SKYeti(),

        // Divine Skills
        new SKAzure()
    };

    private static BitArray? _Accessibility;

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<SkillSystem>();
        _Accessibility = new BitArray(SkillsList.Length);
    }

    internal static int GetRefCount()
    {
        return _SkillRefCount;
    }

    internal static void IncreaseRefCount()
    {
        _SkillRefCount++;
    }

    internal static void GainAccess(int index)
    {
        _Accessibility![index] = true;
    }

    internal static void Lock(int index)
    {
        _Accessibility![index] = false;
    }

    internal static Skill? LocateSkill<T>()
    {
        return  SkillsList.Where(skill => skill.StaticItemType == typeof(T)).Single();
    }

    internal static void StackEnhancementForSkill<T>(int percentageAsWhole) where T : Skill
    {
        throw new NotImplementedException();
    }
}
