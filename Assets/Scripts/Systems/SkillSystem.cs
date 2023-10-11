#nullable enable

using System;
using System.Collections;
using System.Linq;
using static SharedData.Constants;

public sealed class SkillSystem : GameSystem, IActionCategory
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
    };

    public readonly static Skill[] DivineSkillsList =
    {
        new SKAzure(),
        new SKBeastar(),
        new SKBringForthTheGrandInferno(),
        new SKCatastrophicCalamityStream(),
        new SKDevoteYourSoul(),
        new SKJackofaltrade(),
        new SKKagami(),
        new SKPandoraPandora(),
        new SKRoaringThunder(),
        new SKSeptemmare(),
        new SKShowerMeInAThousandRosePetals(),
        new SKSweetDream(),
        new SKYouAreNotAlone()
    };

    public readonly static Skill[] DivineBlessingsList =
    {
        new SKDamselinrede(),
        new SKICanNotImagineAWorldWithoutYou(),
        new SKLastingAmber(),
        new SKOthello(),
        new SKProdigy(),
        new SKShallWeDance(),
        new SKSirensLove(),
        new SKSterling(),
        new SKVengence()
    };

    private static SkillSystemState? _SystemState;

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<SkillSystem>();
        _SystemState = new SkillSystemState(SkillsList.Length);
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
        _SystemState!.skillAcquired[index] = true;
    }

    internal static void Lock(int index)
    {
        _SystemState!.skillAcquired[index] = false;
    }

    internal static Skill? LocateSkill<T>(SkillCategory category = SkillCategory.Standard)
    {
        switch (category)
        {
            case SkillCategory.Standard:
                return SkillsList.Where(skill => skill.StaticItemType == typeof(T)).Single();

            case SkillCategory.Divine:
                return  DivineSkillsList.Where(skill => skill.StaticItemType == typeof(T)).Single();

            case SkillCategory.Blessing:
                return  DivineBlessingsList.Where(skill => skill.StaticItemType == typeof(T)).Single();

            default: return null;
        }
    }

    internal static void StackEnhancementForSkill<T>(int percentageAsWhole) where T : Skill
    {
        throw new NotImplementedException();
    }

    internal static void Save()
    {
        PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].UpdateSkillStateData(_SystemState);
    }
}
