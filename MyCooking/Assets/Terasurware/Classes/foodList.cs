using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class foodList : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int foodIndex;
		public string foodName;
		public string ingredientTextInfo;
		public int ingredientIndex1;
		public int ingredientIndex2;
		public int ingredientIndex3;
		public string CookBowl;
	}
}

