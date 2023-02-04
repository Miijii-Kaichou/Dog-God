#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKYouAreNotAlone : Skill
{
	public override string SkillName => "\"YouAreNotAlone\"";
	public override Type StaticItemType => typeof(SKYouAreNotAlone);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}