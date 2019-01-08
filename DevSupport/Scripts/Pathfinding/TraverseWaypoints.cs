using System.Collections;
using UnityEngine;

public class TraverseWaypoints : MonoBehaviour
{
	// GIVE THE OBJECT A BEGINING OBJECTIVE, THIS WILL SET THE START, INTERMEDIATE, AND END ACTIONS
	public eWaypointAction CurrentWaypointAction;
	public enum eWaypointAction {
		TraverseNext,		// CYCLE THRU THE POINTS IN THE ORDER OF THE ARRAY, TRIGGER END ON THE LAST INDEX
		Random				// GO TO RANDOM POINTS  ON THE ARRAY (NO END EVENT)
	}

	// HANDLES THE CODE TO CALL WHEN THE OBJECT REACHES THE START POSITION
	private eWaypointStartAction CurrentWaypointStartAction;
	public enum eWaypointStartAction {
		TraverseNext,		// WHEN OBJECT REACHES START THEN GO TO THE NEXT POINT
		Random				// WHEN OBJECT REACHES START THEN GO TO A RANDOM POSITION
	}
	
	// HANDLES THE CODE TO CALL WHEN THE OBJECT REACHES THE INTERMEDIATE POSITION
	private eWaypointIntermediateAction CurrentWaypointIntermediateAction;
	public enum eWaypointIntermediateAction {
		TraverseNext,		// GO TO THE NEXT POINT
		Random				// GO TO A RANDOM POINT
	}
	
	// HANDLES THE CODE TO CALL WHEN THE OBJECT REACHES THE END POSITION
	private eWaypointEndAction CurrentWaypointEndAction;
	public enum eWaypointEndAction {
		Destroy,			// DESTROY THE OBJECT AT END
		GoToStart,			// GO TO THE START POINT (THIS IS EQUIVALENT TO TRAVERSE NEXT)
		Random				// GO TO A RANDOM POINT
	}
	
	// HANDLES THE STATE MACHINE
	private eWaypointState CurrentWaypointsState;
	public enum eWaypointState {
		WaitingForPoints,	// DONT MOVE THE OBJECT BECAUSE WE HAVE NO POINTS TO MOVE TO
		WaitingToStart,		// HAVE POINTS, WAITING FOR A START COMMAND (PART OF AN EXTERNAL EVENT)
		Traversing,			// THE OBJECT IS WORKING THROUGH THE POINTS
		GoToStart,			// THE OBJECT IS GOING TO THE START POSITION (ONLY APPLICABLE IMMEDIATELY AFTER WAITINGTOSTART)
		Paused				// PAUSE THE MOVEMENT (PART OF AN EXTERNAL EVENT)
	}

	// OPTIONS FOR STARTING POSITION OF THE OBJECT
	public enum eWhereToStart {
		Current,			// START WHERE THE OBJECT CURRENTLY IS. DON'T MOVE ME
		StartPosition,		// MOVE THIS OBJECT TO THE STARTPOSITION
		EndPosition,		// MOVE THIS OBJECT TO THE ENDPOSITION
		Nearest				// SET THE NEAREST POINT AS THE NEXT POINT TO TRAVERSE TO
	}

	// ORDERED ARRAY OF POINTS TO TRAVERSE (START = 0, END = LENGTH - 1)
	private Vector3[] _Points = null;
	
	// CURRENT POINTS INDEX LOCATION, USED TO KEEP TRACK OF WHAT POINT IS NEXT
	// WHEN PARTOL/ATTACK STATE IS IMPLEMENTED THIS WILL ALLOW THE OBJECT TO EITHER
	// GO PACK TO ITS ORIGINAL POSITION TRAVERSING TO OR NEW LOGIC WILL BE MADE TO
	// TRAVERSE TO THE NEAREST POINT
	private int _CurrentPointsIndex = 0;

	// START WAYPOINT POSITION - RIGHT NOW IS POINTS[0] BUT WILL LATER BE ANY POINT INSIDE POINTS[]
	private Vector3 StartPosition;

	// END WAYPOINT POSITION - RIGHT NOW IS POINTS[LENGTH -1] BUT WILL LATER BE ANY POINT INSIDE POINTS[]
	private Vector3 EndPosition;
	
	// SPEED THAT OBJECT TRAVELS
	public float _TravelSpeed;

    private bool WaypointPaused = false;
    private float WaypointPauseDuration = 2;

	#region UNITY
	void Start()
	{
		//CurrentWaypointsState = eWaypointState.WaitingForPoints;
	}

	void Update()
	{

		// CHECK FOR NULL POINTS, THIS WILL CAUSE THE OBJECT TO WAIT
		if (null == _Points)
			return;
        bool Ready = CurrentWaypointsState == eWaypointState.Traversing;
		// ONLY MOVE WHEN THE STATE IS IN TRAVERSING
		if (Ready)
			Update_Traverse();
	}
	#endregion UNITY

	#region PUBLIC
	public void SetPoints(Vector3[] points, eWaypointAction action, eWhereToStart wheretostart, bool startnow)
	{
		// SET POINTS
		_Points = points;
		StartPosition = _Points[0];
		EndPosition = _Points[_Points.Length - 1];

		// SET THE CURRENT ACTIONS
		SetAction(action);

		// SET THE START POSITION
		switch (wheretostart)
		{
			case eWhereToStart.Current:
				break;
			case eWhereToStart.StartPosition:
				this.transform.position = StartPosition;
				_CurrentPointsIndex = 1;
				break;
			case eWhereToStart.EndPosition:
				this.transform.position = EndPosition;
				_CurrentPointsIndex = 0;
				break;
			case eWhereToStart.Nearest:
				_CurrentPointsIndex = GetClosestIndex(this.transform.position, _Points);
				break;
		}

		// WE HAVE POINTS, NOW WAIT
		CurrentWaypointsState = eWaypointState.WaitingToStart;

		// GET A MOVE ON!
		if (startnow)
		{
			CurrentWaypointsState = eWaypointState.Traversing;
            print("Traverse moving");
		}
	}

	public void SetAction(eWaypointAction action)
	{
		switch (action)
		{
			case eWaypointAction.Random:
				CurrentWaypointStartAction = eWaypointStartAction.Random;
				CurrentWaypointIntermediateAction = eWaypointIntermediateAction.Random;
				CurrentWaypointEndAction = eWaypointEndAction.Random;
				break;
			case eWaypointAction.TraverseNext:
				CurrentWaypointStartAction = eWaypointStartAction.TraverseNext;
				CurrentWaypointIntermediateAction = eWaypointIntermediateAction.TraverseNext;
				CurrentWaypointEndAction = eWaypointEndAction.GoToStart;
				break;
		}
		CurrentWaypointAction = action;
	}

	public eWaypointAction GetAction()
	{
		return CurrentWaypointAction;
	}
	#endregion PUBLIC

	#region PRIVATE
	private void Update_Traverse()
	{
        if (WaypointPaused)
            return;
        
		// MOVE TORWARD THE NEXT POSITION BASED OFF _CURRENTPOINTSINDEX
		var directionToNextPoint = _Points[_CurrentPointsIndex] - this.transform.position;
		var directionToNextPointNormal = directionToNextPoint.normalized;
		this.transform.position += directionToNextPointNormal * _TravelSpeed * Time.smoothDeltaTime;
        this.transform.up = directionToNextPoint.normalized;

		// DETERMINE IF OBJECT IS CLOSE ENOUGH TO THE POINT TO TRIGGER A POINT ACTION
		if (directionToNextPoint.magnitude < .1f)
		{
			// SET OBJECT TO POINT LOCATION. DO THIS FIRST BEFORE POINTSINDEX CHANGES IN THE IF TREE
			this.transform.position = _Points[_CurrentPointsIndex];

			// RUN THE START, INTERMEDIATE, END ACTIONS
			if (_Points[_CurrentPointsIndex] == EndPosition)
				AtEndWaypointAction(CurrentWaypointEndAction);
			else if (_Points[_CurrentPointsIndex] == StartPosition)
				AtStartWaypointAction(CurrentWaypointStartAction);
			else
				AtInterWaypointAction(CurrentWaypointIntermediateAction);

            StartCoroutine("PauseRoutine");
		}
	}

	IEnumerator PauseRoutine()
	{
        // WAIT FOR THE MOD DURATION TO FINISH
        WaypointPaused = true;
		yield return new WaitForSeconds(WaypointPauseDuration);
        WaypointPaused = false;
		yield return null;
	}

	private int GetClosestIndex(Vector3 v, Vector3[] points)
	{
		int wp_index = 0;
		for (int i = 0; i < points.Length; i++)
		{
			if ((v - points[i]).magnitude <= (v - points[wp_index]).magnitude)
			{
				wp_index = i;
			}
		}
		return wp_index;
	}

	private void AtStartWaypointAction(eWaypointStartAction e)
	{
		switch (e)
		{
			case eWaypointStartAction.TraverseNext:
				_CurrentPointsIndex++;
				break;
			case eWaypointStartAction.Random:
				_CurrentPointsIndex = Random.Range(0, _Points.Length);
				break;
		}
	}

	private void AtEndWaypointAction(eWaypointEndAction e)
	{
		switch (e)
		{
			case eWaypointEndAction.Destroy:
				GameObject.Destroy(this.gameObject);
				break;
			case eWaypointEndAction.GoToStart:
				_CurrentPointsIndex = 0;
				break;
			case eWaypointEndAction.Random:
				_CurrentPointsIndex = Random.Range(0, _Points.Length);
				break;
		}
	}

	private void AtInterWaypointAction(eWaypointIntermediateAction e)
	{
		switch (e)
		{
			case eWaypointIntermediateAction.TraverseNext:
				_CurrentPointsIndex++;
				break;
			case eWaypointIntermediateAction.Random:
				_CurrentPointsIndex = Random.Range(0, _Points.Length);
				break;
		}
	}
	#endregion PRIVATE
}
