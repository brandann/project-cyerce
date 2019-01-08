using System.Collections.Generic;

namespace TiledParser
{
	public class MapData
	{
		public enum Layer
		{
			BACKGROUND = 0,		//the back most objects, likely covered by other things
			FLOOR = 1,			//floor path, dubs as the pathfinding map
			WALLS = 2,			//barriers and walls.
			OBJECTS = 3,		//breakables, decor, items, doors
			SPAWNS = 4,			//Enemies, spawnables
			FOREGROUND = 5		//not interactive.Overhead lights, clouds etc.
		}

		private int[,] _MapBackground;
		private int[,] _MapFloor;
		private int[,] _MapWalls;
		private int[,] _MapObjects;
		private int[,] _MapSpawns;
		private int[,] _MapForeground;

		private string mapName;

		public MapData(string name)
		{
			mapName = name;
			LoadMapData();
		}

		public int[,] GetMap(Layer layer)
		{
			switch(layer)
			{
				case Layer.BACKGROUND:
					return _MapBackground;
				case Layer.FLOOR:
					return _MapFloor;
				case Layer.WALLS:
					return _MapWalls;
				case Layer.OBJECTS:
					return _MapObjects;
				case Layer.SPAWNS:
					return _MapSpawns;
				case Layer.FOREGROUND:
					return _MapForeground;
			}

			return null;
		}

		public List<int[,]> GetAllMaps()
		{
			List<int[,]> list = new List<int[,]>();
			list.Add(_MapBackground);
			list.Add(_MapFloor);
			list.Add(_MapWalls);
			list.Add(_MapObjects);
			list.Add(_MapSpawns);
			list.Add(_MapForeground);

			return list;
		}

		private void LoadMapData()
		{

		}
	}
}





