using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct WaveSubset {
	public int count;
	public int id; // The enemy ID
	public int spawnDelay; // Milliseconds
}

[System.Serializable]
public struct Wave
{
	public List<WaveSubset> subsets;
	public List<PathProbability> pathSelection;

	public int Count {
		get { return subsets.Aggregate(0, (acc, t) => acc + t.count); }
	}
}
