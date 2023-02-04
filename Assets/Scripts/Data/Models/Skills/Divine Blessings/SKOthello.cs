#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKOthello : Skill
{
	public override string SkillName => "Othello";
	public override Type StaticItemType => typeof(SKOthello);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}