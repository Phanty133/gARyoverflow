using System;
using System.Collections.Generic;

class ListUtil {

	public static List<T> ArrayToList<T>(T[] arr) {
		List<T> list = new List<T>();

		foreach(T a in arr) {
			list.Add(a);
		}

		return list;
	}

	public static T GetMaxFromList<T>(List<T> list, Func<T, T, bool> isLarger) {
		if(list.Count == 0) return default(T);

		T largest = list[0];
		foreach(T a in list) {
			if(isLarger.Invoke(a, largest)) largest = a;
		} 

		return largest;
	}
}