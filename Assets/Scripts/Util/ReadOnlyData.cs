namespace SharedData
{
    public static class ReadOnlyData
    {
        public static readonly TargetInfo<Item>[] TargetAllItems = new TargetInfo<Item>[20]
        {
            TargetInfo<ITAdulite>.Target(),
            TargetInfo<ITAlguarde>.Target(),
            TargetInfo<ITAlhercule>.Target(),
            TargetInfo<ITChargedAdulite>.Target(),
            TargetInfo<ITEther>.Target(),
            TargetInfo<ITEtherAlpha>.Target(),
            TargetInfo<ITEtherOmega>.Target(),
            TargetInfo<ITMagusCrystal>.Target(),
            TargetInfo<ITMagusPotion>.Target(),
            TargetInfo<ITMagusPotionAlpha>.Target(),
            TargetInfo<ITMagusPotionDelta>.Target(),
            TargetInfo<ITMagusPotionOmega>.Target(),
            TargetInfo<ITMagusShard>.Target(),
            TargetInfo<ITPotion>.Target(),
            TargetInfo<ITPotionAlpha>.Target(),
            TargetInfo<ITPotionDelta>.Target(),
            TargetInfo<ITPotionOmega>.Target(),
            TargetInfo<ITPotionGrande>.Target(),
            TargetInfo<ITPurifiedAdulite>.Target(),
            TargetInfo<ITStellaLeaf>.Target()
        };
    }
}
