#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKJackofaltrade : Skill
{
	public override string SkillName => "Jackofaltrade";
	public override Type StaticItemType => typeof(SKJackofaltrade);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}