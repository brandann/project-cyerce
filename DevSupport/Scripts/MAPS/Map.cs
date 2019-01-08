using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{

	#region Public Methods
	public Map(int[,] map)
	{
		_map = map;
		initMapPath();
	}

	#endregion

	#region public accessors
	public int[,] MapArray
	{
		get { return _map; }
	}
	#endregion

	#region private members
	protected int[,] _map;       // int array of map w/ paths
	#endregion

	private Vector3 FindPointByIndex(int index, int[,] m)
	{
		for (int i = 0; i < m.GetLength(0); i++)
		{
			for (int j = 0; j < m.GetLength(1); j++)
			{
				if (m[i, j] == index)
				{
					return new Vector3(i, j, 0);
				}
			}
		}
		return new Vector3(-1, -1, -1);
	}

	// Mirror the map verically so bool array matches
	// screen map.
	private int[,] FixMap(int[,] m)
	{
		for (int i = 0; i < m.GetLength(0); i++)
		{
			for (int j = 0; j < m.GetLength(1); j++)
			{
				int temp = m[i, j];
				m[i, j] = m[m.GetLength(0) - i - 1, j];
				m[i = m.GetLength(0) - i - 1, j] = temp;
			}

			// if i is >= m.getLength(0) -i -1 then the map has been mirrored to
			// the half point and needs to be returned.
			// if the map continues to mirror it will mirrow back to its
			// origianl layout
			if ((i >= m.GetLength(0) - i))
			{
				return m;
			}
		}
		Debug.LogError("Map Fix is Bad");
		return m; // should never reach this
	}

	private bool[,] MapPath;

	public void SetPathPoint(int x,int y, bool open)
	{
		if (null == MapPath)
			initMapPath();
		MapPath[x, y] = open;
	}

	public bool[,] GetMapPath()
	{
		if (null == MapPath)
			initMapPath();
		return MapPath;
	}

	private void initMapPath()
	{
		MapPath = new bool[_map.GetLength(0), _map.GetLength(1)];
		for (int i = 0; i < MapPath.GetLength(0); i++)
			for (int j = 0; j < MapPath.GetLength(1); j++)
				MapPath[i, j] = false;
	}
}