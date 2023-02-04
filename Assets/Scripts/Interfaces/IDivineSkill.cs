#nullable enable

using System;

public interface IDivineSkill
{
    public Deity? Deity { get; }
    abstract Action? OnSkillActivation { get; set; }
    abstract Action? OnSkillDeactivation { get; set; }
}