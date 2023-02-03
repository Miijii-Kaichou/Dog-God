#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKSweetDream : Skill
{
	public override string SkillName => "Sweet Dream";
	public override Type StaticItemType => typeof(SKSweetDream);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}