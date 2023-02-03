#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKLastingAmber : Skill
{
	public override string SkillName => "Lasting Amber";
	public override Type StaticItemType => typeof(SKLastingAmber);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}