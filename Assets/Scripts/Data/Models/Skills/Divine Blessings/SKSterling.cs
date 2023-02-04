#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKSterling : Skill
{
	public override string SkillName => "Sterling";
	public override Type StaticItemType => typeof(SKSterling);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}