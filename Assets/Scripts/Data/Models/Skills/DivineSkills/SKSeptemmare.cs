#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKSeptemmare : Skill
{
	public override string SkillName => "Septemmare";
	public override Type StaticItemType => typeof(SKSeptemmare);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}