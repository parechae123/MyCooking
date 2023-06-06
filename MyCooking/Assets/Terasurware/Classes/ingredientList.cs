using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ingredientList : ScriptableObject
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
		
		public int index;
		public string name;
		public string englishName;
		public float installerPosX;
		public float installerPosY;
		public float installerPosZ;
	}
}

