using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerDamageOne : UpgradeBase
{
	public override string id { get; } = "slowerUpgradeOne";

	public override string tower { get; } = "bubble";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "damageUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "Makes the enemies slower!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods.Add("slowness", 1);
        multMods.Add("slowness", 1.25f);
	}
}

public class SlowerUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "slowerUpgradeTwo";

	public override string tower { get; } = "bubble";

	public override List<string> prereqs { get; } = new List<string>() { "slowerUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "Are these enemies faster than Unity? I am not sure!!!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["slowness"] += 2;
        multMods["slowness"] *= 1.5f;
	}
}

public class DamageUpgradeOne : UpgradeBase
{
	public override string id { get; } = "damageUpgradeOne";

	public override string tower { get; } = "bubble";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "slowerUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "Enemies are weakened in presence of your aura!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
        multMods.Add("damageInc", 1.5f);
	}
}

public class DamageUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "damageUpgradeTwo";

	public override string tower { get; } = "bubble";

	public override List<string> prereqs { get; } = new List<string>() { "damageUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "wow the enemies are weak";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
        multMods["damageInc"] *= 1.5f;
	}
}