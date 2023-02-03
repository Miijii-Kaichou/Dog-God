#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKICanNotImagineAWorldWithoutYou : Skill
{
	public override string SkillName => "\"I Can Not Imagine A World Without You\"";
	public override Type StaticItemType => typeof(SKICanNotImagineAWorldWithoutYou);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}