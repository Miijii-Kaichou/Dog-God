#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKCatastrophicCalamityStream : Skill
{
	public override string SkillName => "Catastrophic Calamity Stream";
	public override Type StaticItemType => typeof(SKCatastrophicCalamityStream);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}