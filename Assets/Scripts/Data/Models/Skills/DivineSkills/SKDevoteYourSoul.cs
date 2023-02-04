#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKDevoteYourSoul : Skill
{
	public override string SkillName => "\"Devote Your Soul\"";
	public override Type StaticItemType => typeof(SKDevoteYourSoul);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}