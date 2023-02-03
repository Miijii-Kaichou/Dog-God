#nullable enable

using System;
using static SharedData.Constants;

///<summary>
///
///</summary>
public sealed class SKShowerMeInAThousandRosePetals : Skill
{
	public override string SkillName => "\"Shower Me InA Thousand Rose Petals\"";
	public override Type StaticItemType => typeof(SKShowerMeInAThousandRosePetals);
	public override ItemUseCallback OnActionUse => UseSkill;

	private void UseSkill()
	{
		throw new NotImplementedException();
	}
}