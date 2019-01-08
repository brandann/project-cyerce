using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMap {

    public string SA_TEXT;
    public string SB_TEXT;
    public string SC_TEXT;
    public string SD_TEXT;

    public const int ERROR = -1;
	public const int WALL = 0;
	public const int OPEN_PATH = 1;

	public const int KEY_RED = 2;
	public const int KEY_ORANGE = 3;
	public const int KEY_YELLOW = 4;
	public const int KEY_GREEN = 5;
	public const int KEY_BLUE = 6;
	public const int KEY_PURPLE = 7;

	public const int START_NODE = 8;
	public const int END_NODE = 9;

	public const int DOOR_RED = 10;
	public const int DOOR_ORANGE = 11;
	public const int DOOR_YELLOW = 12;
	public const int DOOR_GREEN = 13;
	public const int DOOR_BLUE = 14;
	public const int DOOR_PURPLE = 15;

	public const int COLLECTION = 16;
	public const int MONSTER = 17;

	public const int DOOR_RANDOM = 18;

    public const int SIGN_SA = 19;
    public const int SIGN_SB = 20;
    public const int SIGN_SC = 21;
    public const int SIGN_SD = 22;

	public const int LASERHEAD_CLOCKWISE = 23;

	public const int ENEMY_PACE_UP = 24;
	public const int ENEMY_PACE_DOWN = 25;
	public const int ENEMY_PACE_LEFT = 26;
	public const int ENEMY_PACE_RIGHT = 27;

	public const int HP_BOOST = 28;

	public const int ENEMY_PACE_VERTICAL = 29;
	public const int ENEMY_PACE_HORIZONTAL = 30;

	public const int LASERHEAD_COUNTER = 31;

	public const int BOOST_NORTH = 32;
	public const int BOOST_NORTHEAST = 33;
	public const int BOOST_EAST = 34;
	public const int BOOST_SOUTHEAST = 35;
	public const int BOOST_SOUTH = 36;
	public const int BOOST_SOUTHWEST = 37;
	public const int BOOST_WEST = 38;
	public const int BOOST_NORTHWEST = 39;

	public const int WALL_Q1 = 40;
	public const int WALL_Q2 = 41;
	public const int WALL_Q3 = 42;
	public const int WALL_Q4 = 43;

    public const int LAVA_BLOCK_BOTTOM = 44;
    public const int LAVA_BLOCK_LEFT = 45;
	public const int LAVA_BLOCK_TOP = 46;
	public const int LAVA_BLOCK_RIGHT = 47;

	public const int WATCH_TV = 48;

    public const int TURRET_UP = 49;
    public const int TURRET_DOWN = 50;
    public const int TURRET_LEFT = 51;
    public const int TURRET_RIGHT = 52;

	public virtual Map GetMap()
	{
		return null;
	}

	public virtual int[,] GetBackground()
	{
		return null;
	}

	public virtual string GetLevelEndQuote()
	{
		return "";
	}
}