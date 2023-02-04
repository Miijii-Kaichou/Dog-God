#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKSirensLove : Skill
{
	public override string SkillName => "Sirens Love";
	public override Type StaticItemType => typeof(SKSirensLove);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}