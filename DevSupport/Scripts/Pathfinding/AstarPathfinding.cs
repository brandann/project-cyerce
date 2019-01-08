using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AstarPathfinding {
	
	public AstarPathfinding()
	{

	}

	#region PUBLIC

	///<summary>
	///<para>Takes a list of AstarNodes and returns a recurive node path of the shortest path</para>
	///<para>Returns: AstarNode shorest path node, null is no path</para>
	///</summary>
	public static AstarNode GetShortestPath(List<AstarNode> nodes)
	{
		return GetShortestPath(nodes.ToArray());
	}

	///<summary>
	///<para>Takes an array of AstarNodes and returns a recurive node path of the shortest path</para>
	///<para>Returns: AstarNode shorest path node, null is no path</para>
	///</summary>
	public static AstarNode GetShortestPath(AstarNode[] nodes)
	{
		var openList = new List<AstarNode>();
		
		var Map = CreateMapFromNodes(nodes);

		var startPosition = GetPosition(Map, TOKEN_START);
		var endPosition = GetPosition(Map, TOKEN_END);
		SetAllHValues(Map, endPosition);

		AstarNode current = Map.GetNodeAt((int)startPosition.x, (int)startPosition.y);
		current.State = AstarNode.state.CLOSED;
		while (!current.endposition)
		{
			var l = GetAdjacentWalkableNodes(Map, current);
			foreach (var n in l)
				if (!openList.Contains(n))
					openList.Add(n);

			current = MinNode(openList);
			if (current == null)
				return null;
			current.State = AstarNode.state.CLOSED;
			openList.Remove(current);
		}
		return current;
	}

	///<summary>
	///<para>Takes a array of Gameobjects with Component AstarNode and returns a recurive node path of the shortest path</para>
	///<para>Returns: AstarNode shorest path node, null is no path</para>
	///</summary>
	public static AstarNode GetShortestPath(GameObject[] gos)
	{
		List<AstarNode> nodes = new List<AstarNode>();

		foreach(var n in gos)
		{
			var wsn = n.GetComponent<AstarTileNode>();
			var node = new AstarNode(
				(int) n.transform.position.x,
				(int) n.transform.position.y,
				wsn.CurrentNodeState != AstarTileNode.AstarNodeState.Closed,
				wsn.CurrentNodeState == AstarTileNode.AstarNodeState.Start,
				wsn.CurrentNodeState == AstarTileNode.AstarNodeState.End
			);
			nodes.Add(node);
		}
		
		return GetShortestPath(nodes);
	}

	#endregion

	#region PRIVATE
	///<summary>
	///<para>Uses an array of AstarNodes to create a map with a base location not at 0,0</para>
	///</summary>
	private static MapWrapper CreateMapFromNodes(AstarNode[] nodes)
	{
		var p = BottomLeft(nodes);
		var h = (int) (TopLeft(nodes) - p).magnitude + 1;
		var w = (int)(BottomRight(nodes) - p).magnitude + 1;

		var map = new MapWrapper(w, h);
		
		foreach(var n in nodes)
		{
			int x = (int)(n.Position.x - p.x);
			int y = (int)(n.Position.y - p.y);
			map.SetNodeAt(x, y, n);
		}

		return map;
	}

	///<summary>
	///<para>Gets the min x and min y positions from the nodes and returns a bottom left vec2</para>
	///</summary>
	private static Vector2 BottomLeft(AstarNode[] nodes)
	{
		var x = GetMinx(nodes);
		var y = GetMiny(nodes);
		return new Vector2(x, y);
	}

	///<summary>
	///<para>Gets the min x and max y positions from the nodes and returns a top left vec2</para>
	///</summary>
	private static Vector2 TopLeft(AstarNode[] nodes)
	{
		var x = GetMinx(nodes);
		var y = GetMaxy(nodes);
		return new Vector2(x, y);
	}

	///<summary>
	///<para>Gets the max x and min y positions from the nodes and returns a bottom right vec2</para>
	///</summary>
	private static Vector2 BottomRight(AstarNode[] nodes)
	{
		var x = GetMaxx(nodes);
		var y = GetMiny(nodes);
		return new Vector2(x, y);
	}

	///<summary>
	///<para>Gets the max x and max y positions from the nodes and returns a top right vec2</para>
	///</summary>
	private static Vector2 TopRight(AstarNode[] nodes)
	{
		var x = GetMaxx(nodes);
		var y = GetMaxy(nodes);
		return new Vector2(x, y);
	}

	///<summary>
	///<para>Gets the min x position from the nodes</para>
	///</summary>
	private static int GetMinx(AstarNode[] nodes)
	{
		float x = float.MaxValue;
		foreach(var n in nodes)
			if (n.Position.x < x)
				x = n.Position.x;
		return (int)x;
	}

	///<summary>
	///<para>Gets the min y position from the nodes</para>
	///</summary>
	private static int GetMiny(AstarNode[] nodes)
	{
		float y = float.MaxValue;
		foreach (var n in nodes)
			if (n.Position.y < y)
				y = n.Position.y;
		return (int)y;
	}

	///<summary>
	///<para>Gets the max x position from the nodes</para>
	///</summary>
	private static int GetMaxx(AstarNode[] nodes)
	{
		float x = float.MinValue;
		foreach (var n in nodes)
			if (n.Position.x > x)
				x = n.Position.x;
		return (int)x;
	}

	///<summary>
	///<para>Gets the max y position from the nodes</para>
	///</summary>
	private static int GetMaxy(AstarNode[] nodes)
	{
		float y = float.MinValue;
		foreach (var n in nodes)
			if (n.Position.y > y)
				y = n.Position.y;
		return (int)y;
	}

	///<summary>
	///<para>Get the smallest F value AstarNode from a list of nodes</para>
	///</summary>
	private static AstarNode MinNode(List<AstarNode> nodes)
	{
		if (nodes.Count == 0)
			return null;
		AstarNode current = nodes[0];
		foreach (var n in nodes)
			if (n.F < current.F)
				current = n;
		return current;
	}

	///<summary>
	///<para>Set all the node H values</para>
	///</summary>
	private static void SetAllHValues(MapWrapper map, Vector2 end)
	{
		for (int y = 0; y < map.GetYlength(); y++)
			for (int x = 0; x < map.GetXlength(); x++)
				map.GetNodeAt(x, y).H = (end - new Vector2(x, y)).magnitude;
	}

	///<summary>
	///<para>Gets the first vec2 position of a node with the CHAR_TOKEN == token</para>
	///</summary>
	private static Vector2 GetPosition(MapWrapper map, char token)
	{
		var mapXlength = map.GetXlength();
		var mapYlength = map.GetYlength();
		for (int y = 0; y < mapYlength; y++)
			for (int x = 0; x < mapXlength; x++)
				if (map.GetNodeAt(x,y).GetToken() == token)
					return new Vector2(x, y);
		return new Vector2(-1, -1);
	}

	///<summary>
	///<para>Gets a list of node neigboring the fromnode that are open</para>
	///</summary>
	private static List<AstarNode> GetAdjacentWalkableNodes(MapWrapper map, AstarNode fromNode)
	{
		List<AstarNode> open = new List<AstarNode>();
		var nextLocations = GetAdjacentLocations(fromNode.Position, map);
		
		foreach (var location in nextLocations)
		{
			int x = (int)location.Position.x;
			int y = (int)location.Position.y;

			// Stay within the grid's boundaries
			if (x < 0 || x >= map.GetXlength() || y < 0 || y >= map.GetYlength())
				continue;

			var node = map.GetNodeAt(x, y);
			
			// Ignore non-walkable nodes
			if (!node.passable)
				continue;

			// Ignore already-closed nodes
			if (node.State == AstarNode.state.CLOSED)
				continue;

			// Already-open nodes are only added to the list if their G-value is lower going via this route.
			if (node.State == AstarNode.state.OPEN)
			{
				float traversalCost = AstarNode.GetTraversalCost(node, node.ParentNode);
				float gTemp = fromNode.G + traversalCost;
				if (gTemp < node.G)
				{
					node.ParentNode = fromNode;
					open.Add(node);
				}
			}
			else
			{
				// If it's untested, set the parent and flag it as 'Open' for consideration
				node.ParentNode = fromNode;
				node.State = AstarNode.state.OPEN;
				open.Add(node);
			}
		}
		return open;
	}

	///<summary>
	///<para>Gets all the cordinates from the map adjacent to v</para>
	///</summary>
	private static AstarNode[] GetAdjacentLocations(Vector2 v, MapWrapper map)
	{
		int x, y;
		List<AstarNode> l = new List<AstarNode>();
		var mapXlength = map.GetXlength();
		var mapYlength = map.GetYlength();
		for (int dy = -1; dy < 2; dy++)
		{
			for (int dx = -1; dx < 2; dx++)
			{
				x = dx + (int)v.x;
				y = dy + (int)v.y;
				if (x < 0 || x >= mapXlength || y < 0 || y >= mapYlength)
					continue;
				l.Add(map.GetNodeAt(x, y));
			}
		}
		return l.ToArray();
	}
	#endregion
	
	private class MapWrapper
    {
        private AstarNode[,] map;

		///<summary>
		///<para>Empty Constructor</para>
		///</summary>
		public MapWrapper() { }

		///<summary>
		///<para>Constructor with Width and Height of map</para>
		///</summary>
		public MapWrapper(int width, int height) { map = new AstarNode[height, width]; }

		///<summary>
		///<para>Constructor from existing Node 2D Array</para>
		///</summary>
		public MapWrapper(AstarNode[,] m) { map = m; }

		///<summary>
		///<para>Get Node at x,y position</para>
		///</summary>
		public AstarNode GetNodeAt(int x, int y) { return map[y, x]; }

		///<summary>
		///<para>Set Node at x,y position</para>
		///</summary>
		public void SetNodeAt(int x, int y, AstarNode n) { map[y, x] = n; }

		///<summary>
		///<para>Set map from existing Node 2D Array</para>
		///</summary>
		public void SetMap(AstarNode[,] m) { map = m; }

		///<summary>
		///<para>Get AstarNodeMap</para>
		///</summary>
		public AstarNode[,] GetMap() { return map; }

		///<summary>
		///<para>Get X Map Length (Width)</para>
		///</summary>
		public int GetXlength() { return map.GetLength(1); }

		///<summary>
		///<para>Get Y Map Length (Height)</para>
		///</summary>
		public int GetYlength() { return map.GetLength(0); }
    }
	
	public const char TOKEN_OPEN = 'o';
	public const char TOKEN_CLOSED = 'c';
	public const char TOKEN_START = 's';
	public const char TOKEN_END = 'e';
}

public class AstarNode : IComparer
{
	public enum state { OPEN, CLOSED, UNTESTED }
	private Vector2 _position;

	public bool passable = false;
	public bool startposition = false;
	public bool endposition = false;
	public state State;

	private AstarNode parent;
	private float hValue = 0;
	private float gValue = 0;

	///<summary>
	///<para>Constructor</para>
	///</summary>
	public AstarNode(int xpos, int ypos, bool ispassable, bool isstartposition, bool isendposition)
	{
		_position = new Vector2(xpos, ypos);
		this.passable = ispassable;
		this.startposition = isstartposition;
		this.endposition = isendposition;
		State = state.UNTESTED;
	}

	///<summary>
	///<para>Reference to Parent Node, Null == No Parent</para>
	///</summary>
	public AstarNode ParentNode
	{
		get { return parent; }
		set
		{
			parent = value;
			this.G = this.parent.G + GetTraversalCost(this, this.parent);
		}
	}

	///<summary>
	///<para>Distance from a node to a target node</para>
	///</summary>
	public float H
	{
		get { return hValue; }
		set { hValue = value; }
	}
	
	///<summary>
	///<para>Movement Cost from the node to another node</para>
	///</summary>
	public float G
	{
		get { return gValue; }
		set { gValue = value; }
	}

	///<summary>
	///<para>Value: Movement Cost + Distance (G+H)</para>
	///</summary>
	public float F
	{
		get { return G + H; }
		private set { }
	}

	///<summary>
	///<para>Compares 2 nodes F Values</para>
	///</summary>
	public int Compare(object x, object y)
	{
		return ((AstarNode)x).F.CompareTo(((AstarNode)y).F);
	}

	///<summary>
	///<para>World Position of the node</para>
	///</summary>
	public Vector2 Position
	{
		set { _position = new Vector2(value.x, value.y); }
		get { return _position; }
	}

	///<summary>
	///<para>Traversal Cost (default Vert/Horiz = 1, Diagnal = 1.4)</para>
	///</summary>
	public static float GetTraversalCost(AstarNode node, AstarNode otherNode)
	{
		float deltaX = otherNode.Position.x - node.Position.x;
		float deltaY = otherNode.Position.y - node.Position.y;
		return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
	}

	///<summary>
	///<para>Get Nodes Char token equivalent</para>
	///</summary>
	public char GetToken()
	{
		if (startposition)
			return AstarPathfinding.TOKEN_START;
		if (endposition)
			return AstarPathfinding.TOKEN_END;
		if (passable)
			return AstarPathfinding.TOKEN_OPEN;
		return AstarPathfinding.TOKEN_CLOSED;
	}
}