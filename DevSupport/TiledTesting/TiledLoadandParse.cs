using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledParser
{
	public class TiledLoadandParse
	{
		public TiledLoadandParse(string path)
		{
			var raw_lines = LoadRawFile(path); // READ THE FILE IN AS STRING LINES
			var clean_raw_lines = ClearMeta(raw_lines); // REMOVE THE META DATA BEFORE THE FIRST <LAYER NAME INSTANCE
			var layer = MakeLayers(clean_raw_lines);

			foreach (string s in clean_raw_lines)
			{
				//Console.WriteLine(s);
			}

			foreach (LAYER l in layer)
			{
				Console.WriteLine("NAME=" + l.name);
				Console.WriteLine("WIDTH=" + l.width);
				Console.WriteLine("HEIGHT=" + l.height);
			}
				
		}

		// READ THE FILE IN AS STRING LINES
		private string[] LoadRawFile(string path)
		{
			return System.IO.File.ReadAllLines(path);
		}

		// REMOVE THE META DATA BEFORE THE FIRST <LAYER NAME INSTANCE
		private string[] ClearMeta(string[] lines)
		{
			List<string> new_lines = new List<string>();
			bool endedMeta = false;
			for(int i = 0; i < lines.Length; i++)
			{
				if(lines[i].Contains("<layer name"))
				{
					endedMeta = true;
				}

				if(endedMeta)
				{
					new_lines.Add(lines[i]);
				}
			}

			return new_lines.ToArray();
		}

		// GET THE INDIVIDUAL LAYERS FROM THE FILE
		private LAYER[] MakeLayers(string[] lines)
		{
			
			List<string[]> layer_groups = new List<string[]>();
			List<string> group = new List<string>();

			LAYER layer = new LAYER();
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Contains("layer name"))
				{
					group = new List<string>();
				}

				group.Add(lines[i]);

				if (lines[i].Contains("</data"))
				{
					layer_groups.Add(group.ToArray());
				}
				
			}

			List<LAYER> layers = new List<LAYER>();
			foreach (var g in layer_groups)
				layers.Add(makeLayer(g));
			
			return layers.ToArray();
		}

		private LAYER makeLayer(string[] group)
		{
			LAYER layer = new LAYER();
			string line = group[0].Replace(" ", ",");
			string[] split = line.Split(',');

			// GET THE LAYER NAME
			var name = split[2].Replace("name=", "");
			name = name.Replace("\"", "");
			layer.name = name;

			// GET THE LAYER WIDTH
			var width = split[3].Replace("width=", "");
			width = width.Replace("\"", "");
			layer.width = int.Parse(width);

			// GET THE LAYER HEIGHT
			var height = split[4].Replace("height=", "");
			height = height.Replace("\"", "");
			height = height.Replace(">", "");
			layer.height = int.Parse(height);

			int[,] int_list = new int[layer.height, layer.width];

			
			for (int i = 2; i < group.Length-1; i++)
			{
				// TODO READ THE CHAR ARRAY
				string[] group_split = group[i].Split(',');
				for(int j = 0; j < group_split.Length-1; j++)
				{
					int_list[i,j] = int.Parse(group_split[j]);
				}
			}

			layer.grid = int_list;
			
			return layer;
		}

		public struct LAYER
		{
			public string name;
			public int width;
			public int height;
			public int[,] grid;
		}
	}

	
}
