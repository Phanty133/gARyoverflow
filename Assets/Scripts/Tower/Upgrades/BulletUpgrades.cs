using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgradeOne : UpgradeBase
{
	public override string id { get; } = "speedUpgradeOne";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "sniperUpgradeOne", "chungusUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "Shoots bullets much faster!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods.Add("firerate", 1);
        multMods.Add("firerate", 1.25f);
	}
}

public class SpeedUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "speedUpgradeTwo";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>() { "speedUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "Shoots bullets much, much faster!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["firerate"] += 2;
        multMods["firerate"] *= 1.5f;
	}
}

public class SpeedUpgradeThree : UpgradeBase
{
	public override string id { get; } = "speedUpgradeThree";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>() { "speedUpgradeTwo" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "Shoots TWO bullets, yowza!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["firerate"] += 2;
        multMods["firerate"] *= 1.5f;

        // TODO: add double shot flag or sth
	}
}

public class SniperUpgradeOne : UpgradeBase
{
	public override string id { get; } = "sniperUpgradeOne";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "speedUpgradeOne", "chungusUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "Shoots bullets further!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods.Add("lifetime", 3);
        multMods.Add("range", 1.5f);
	}
}

public class SniperUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "sniperUpgradeTwo";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>() { "sniperUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "Shoots bullets much, much further!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["lifetime"] += 3;
        multMods["range"] *= 1.5f;
        flatMods.Add("pierce", 1);
	}
}

public class SniperUpgradeThree : UpgradeBase
{
	public override string id { get; } = "sniperUpgradeThree";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>() { "sniperUpgradeTwo" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "Incredible pierce for incredible gains!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["lifetime"] += 3;
        multMods["range"] *= 1.5f;
        flatMods["pierce"] += 2;

        // TODO: add double shot flag or sth
	}
}

public class ChungusUpgradeOne : UpgradeBase
{
	public override string id { get; } = "chungusUpgradeOne";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>();

	public override List<string> exclusive { get; } = new List<string>(){ "speedUpgradeOne", "sniperUpgradeOne" };

	public override int cost { get; } = 0;

	public override string description { get; } = "SHOOTS BIG";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods.Add("damage", 10);
        multMods.Add("pierce", 2f);
	}
}

public class ChungusUpgradeTwo : UpgradeBase
{
	public override string id { get; } = "chungusUpgradeTwo";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>() { "chungusUpgradeOne" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "BIG BIG SHOOT";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["damage"] += 10;
        multMods["damage"] *= 1.75f;
	}
}

public class ChungusUpgradeThree : UpgradeBase
{
	public override string id { get; } = "chungusUpgradeThree";

	public override string tower { get; } = "bullet";

	public override List<string> prereqs { get; } = new List<string>() { "chungusUpgradeTwo" };

	public override List<string> exclusive { get; } = new List<string>();

	public override int cost { get; } = 0;

	public override string description { get; } = "This is one BIG bullet!";

	public override void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods)
	{
		flatMods["damage"] += 10;
        multMods["damage"] *= 2f;
        multMods["pierce"] *= 3;

        flatMods.Add("shotIncrease", 1);
	}
}