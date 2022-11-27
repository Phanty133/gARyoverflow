using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeBase
{
    public abstract string id { get; }
    public abstract string tower { get; }
    public abstract List<string> prereqs { get; }
    public abstract List<string> exclusive { get; }
    public abstract int cost { get; }
    public abstract string description { get; }
    
    public abstract void ApplyUpgrade(ref Dictionary<string, float> flatMods, ref Dictionary<string, float> multMods);
}
