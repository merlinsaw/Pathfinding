using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarPathfinding
{
  public class Node
  {
	public int GridX;
	public int GridY;

	public bool IsWall;
	public Vector3 Position;

	public Node Parent;

	public int GCost;
	public int HCost;

	public int FCost { get => GCost + HCost; }

	public Node(bool isWall, Vector3 pos, int gridX, int gridY)
	{
	  IsWall = isWall;
	  Position = pos;
	  GridX = gridX;
	  GridY = gridY;
	}
  }
}
