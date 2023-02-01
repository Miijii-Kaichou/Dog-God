interface IDependent
{
    // (Only for Divine Blessings), this will let you know that this should only be activated if a target skill is active.
    public Skill TargetSkill { get; }
    void OnSkillUse();
}
