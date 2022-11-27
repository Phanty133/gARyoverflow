using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpgradeOne : UpgradeBase
{
	public override string id { get; } = "powerUpgradeOne";

	public override string tower { get; } = "laser";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "versatilityUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "Don't let it overcharge!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods.Add("damage", 1);
        multMods.Add("damage", 1.25f);
	}
}

public class PowerUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "powerUpgradeTwo";

	public override string tower { get; } = "laser";

	public override List<string> prereqs { get; } = new List<string>() { "powerUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "Overcharge? What do you mean, overchar--";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["firerate"] += 2;
        multMods["firerate"] *= 1.5f;
	}
}

public class VersatilityUpgradeOne : UpgradeBase
{
	public override string id { get; } = "versatilityUpgradeOne";

	public override string tower { get; } = "laser";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "powerUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "Increases the range of the laser, woag!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
        multMods.Add("range", 1.5f);
	}
}

public class VersatilityUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "versatilityUpgradeTwo";

	public override string tower { get; } = "laser";

	public override List<string> prereqs { get; } = new List<string>() { "versatilityUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "AYO WHAT IS THAT LASER DOING";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
        multMods["range"] *= 1.5f;

        // TODO: Laser starts pointing at stuff flag
	}
}