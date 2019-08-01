using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Heap;

namespace AStarPathfinding
{
  public class Node : IHeapItem<Node>
  {
	public int GridX;
	public int GridY;

	public bool IsWall;
	public Vector3 Position;

	public Node Parent;

	public int GCost;
	public int HCost;

	public int FCost { get => GCost + HCost; }

	private int heapIndex;
	public int HeapIndex { get => heapIndex; set => heapIndex = value; }

	public Node(bool isWall, Vector3 pos, int gridX, int gridY)
	{
	  IsWall = isWall;
	  Position = pos;
	  GridX = gridX;
	  GridY = gridY;
	}

	public int CompareTo(Node nodeToCompare)
	{
	  int compare = FCost.CompareTo(nodeToCompare.FCost);
	  if (compare == 0)
	  {
		compare = HCost.CompareTo(nodeToCompare.HCost);
	  }
	  return -compare;
	}
  }
}
