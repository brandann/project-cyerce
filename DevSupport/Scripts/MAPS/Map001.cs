using UnityEngine;																														
using System.Collections;																														

public class Map001 : GameMap {
	public override Map GetMap ()
	{
        int[,] intmap = new int[,] {
{-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1},
{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,0},
{0,1,1,1,1,1,1,19,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,0},
{0,1,8,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,9,1,1,0},
{0,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,10,1,1,1,1,1,1,0},
{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,0},
{-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1
}

		};

        Map map = new Map(intmap);

        SA_TEXT = "Welcome To Project Aegires! Collect Keys to Find your happiness.";

        return map;
    }

	public override int[,] GetBackground()
	{
		int[,] intbackground = new int[,] { };
		return intbackground;
	}

	public override string GetLevelEndQuote()
	{
		return "'The difference between what we do and what we are capable of doing would suffice to solve most of the world’s problems.'\n~ Mahatma Gandhi";
	}
}