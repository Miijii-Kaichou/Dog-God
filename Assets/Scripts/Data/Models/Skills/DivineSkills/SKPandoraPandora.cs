#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKPandoraPandora : Skill
{
	public override string SkillName => "Pandora-Pandora";
	public override Type StaticItemType => typeof(SKPandoraPandora);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}