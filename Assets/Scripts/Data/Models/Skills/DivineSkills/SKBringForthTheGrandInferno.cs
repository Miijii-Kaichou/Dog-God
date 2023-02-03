#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKBringForthTheGrandInferno : Skill
{
	public override string SkillName => "\"Bring Forth The Grand Inferno\"";
	public override Type StaticItemType => typeof(SKBringForthTheGrandInferno);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}