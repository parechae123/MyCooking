using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class DataTable_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/04.ExelData/DataTable.xls";
	private static readonly string exportPath = "Assets/04.ExelData/DataTable.asset";
	private static readonly string[] sheetNames = { "food", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			foodList data = (foodList)AssetDatabase.LoadAssetAtPath (exportPath, typeof(foodList));
			if (data == null) {
				data = ScriptableObject.CreateInstance<foodList> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					foodList.Sheet s = new foodList.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						foodList.Param p = new foodList.Param ();
						
					cell = row.GetCell(0); p.foodIndex = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.foodName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.ingredientTextInfo = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.ingredientIndex1 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.ingredientIndex2 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.ingredientIndex3 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.CookBowl = (cell == null ? "" : cell.StringCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
