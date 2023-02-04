#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKVengence : Skill
{
	public override string SkillName => "Vengence";
	public override Type StaticItemType => typeof(SKVengence);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}