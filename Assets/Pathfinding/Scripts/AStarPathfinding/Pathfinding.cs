using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Common.Heap;

namespace AStarPathfinding
{
/// <summary>
/// 
/// </summary>
  public class Pathfinding : MonoBehaviour
  {
	Grid GridReference;
	public Transform StartPosition;
	public Transform TargetPosition;

	private void Awake()
	{
	  Cashing();
	}

	private void Update()
	{
	  if (Input.GetButtonDown("Jump"))
	  {
		FindPath(StartPosition.position, TargetPosition.position);
	  }
	  
	}

	private void Cashing()
	{
	  GridReference = GetComponent<Grid>();
	}

	void FindPath(Vector3 starPosition, Vector3 targetPosition)
	{
	  Stopwatch sw = new Stopwatch();
	  sw.Start();
	  Node StartNode = GridReference.NodeFromWorldPosition(starPosition);
	  Node TargetNode = GridReference.NodeFromWorldPosition(targetPosition);

	  //List<Node> Openset = new List<Node>();
	  Heap<Node> OpenSet = new Heap<Node>(GridReference.MaxSize); //TODO: really understand the heap
	  HashSet<Node> ClosedSet = new HashSet<Node>();//TODO: why is a hashset used here? maybe because we use ClosedList.Containes aftewards

	  OpenSet.Add(StartNode);

	  while (OpenSet.Count > 0)
	  {
		Node CurrentNode = OpenSet.RemoveFirst();
		//Node CurrentNode = OpenSet[0];
		//for (int node = 1; node < OpenSet.Count; node++)
		//{
		//  if (OpenSet[node].FCost < CurrentNode.FCost || OpenSet[node].FCost == CurrentNode.FCost && OpenSet[node].HCost < CurrentNode.HCost)
		//  {
		//	CurrentNode = OpenSet[node];
		//  }
		//}
		//OpenSet.Remove(CurrentNode);
		ClosedSet.Add(CurrentNode);

		if (CurrentNode == TargetNode)
		{
		  sw.Stop();
		  print("Path found: " + sw.ElapsedMilliseconds + " ms");
		  GetFinalPath(StartNode, TargetNode);
		  return;
		}

		foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))
		{
		  if (!NeighborNode.IsWall || ClosedSet.Contains(NeighborNode))
		  {
			continue;
		  }
		  int MoveCost = CurrentNode.GCost + GetManhattenDistance(CurrentNode, NeighborNode);

		  if (MoveCost < NeighborNode.GCost || !OpenSet.Contains(NeighborNode))
		  {
			NeighborNode.GCost = MoveCost;
			NeighborNode.HCost = GetManhattenDistance(NeighborNode, TargetNode);
			NeighborNode.Parent = CurrentNode; // TODO: verstehe ich nicht ganz
		  }

		  if (!OpenSet.Contains(NeighborNode))
		  {
			OpenSet.Add(NeighborNode);
		  }
		}
	  }
	}

	private void GetFinalPath(Node startingNode, Node endNode)
	{
	  List<Node> FinalPath = new List<Node>();
	  Node CurrentNode = endNode;

	  while (CurrentNode != startingNode)
	  {
		FinalPath.Add(CurrentNode);
		CurrentNode = CurrentNode.Parent;
	  }

	  FinalPath.Reverse();

	  GridReference.FinalPath = FinalPath;
	}

	private int GetManhattenDistance(Node nodeA, Node nodeB)
	{
	  int ix = Mathf.Abs(nodeA.GridX - nodeB.GridX);
	  int iy = Mathf.Abs(nodeA.GridY - nodeB.GridY);

	  return ix + iy;
	}
  }
}