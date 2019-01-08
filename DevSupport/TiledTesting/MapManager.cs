using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledParser
{
	public class MapManager
	{
		MapData[] MapList;

		static private MapManager _mapManager;
		static public MapManager Instance
		{
			get { if (null == _mapManager)
					_mapManager = new MapManager();
				return _mapManager;
			}
		}

		public MapManager()
		{
			List<MapData> list = new List<MapData>();
			list.Add(new MapData("test"));

			MapList = list.ToArray();
		}
	}
}
