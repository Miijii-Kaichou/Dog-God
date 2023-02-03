#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKKagami : Skill
{
	public override string SkillName => "Kagami";
	public override Type StaticItemType => typeof(SKKagami);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}