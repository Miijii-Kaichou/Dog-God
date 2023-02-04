#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKProdigy : Skill
{
	public override string SkillName => "Prodigy";
	public override Type StaticItemType => typeof(SKProdigy);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}