#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKDamselinrede : Skill
{
	public override string SkillName => "Damselinrede";
	public override Type StaticItemType => typeof(SKDamselinrede);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}