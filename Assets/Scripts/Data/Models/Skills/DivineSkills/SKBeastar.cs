#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKBeastar : Skill
{
	public override string SkillName => "Beastar";
	public override Type StaticItemType => typeof(SKBeastar);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}