#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKRoaringThunder : Skill
{
	public override string SkillName => "Roaring Thunder";
	public override Type StaticItemType => typeof(SKRoaringThunder);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}