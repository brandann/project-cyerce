using UnityEngine;																														
using System.Collections;																														

public class Map107 : GameMap {
	public override Map GetMap ()
	{
        int[,] intmap = new int[,] {
{-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1},
{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,0,1,1,1,1,1,1,9,1,1,1,1,1,1,0,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0},
{0,1,1,16,1,1,1,1,1,1,23,1,1,1,0,1,1,8,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,1,1,0,1,1,1,1,1,1,16,1,1,1,1,1,1,0,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
{-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1
}

};

        Map map = new Map(intmap);

        return map;
    }

	public override int[,] GetBackground()
	{
		int[,] intbackground = new int[,] { };
		return intbackground;
	}
}