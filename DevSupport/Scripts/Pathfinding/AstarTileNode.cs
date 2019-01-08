using UnityEngine;
using System.Collections;

public class AstarTileNode : MonoBehaviour {
	public enum AstarNodeState { Open, Closed, Start, End}
	public AstarNodeState CurrentNodeState;
}
