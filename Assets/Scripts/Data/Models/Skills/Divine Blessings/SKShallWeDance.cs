#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKShallWeDance : Skill
{
	public override string SkillName => "\"Shall We Dance\"";
	public override Type StaticItemType => typeof(SKShallWeDance);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}