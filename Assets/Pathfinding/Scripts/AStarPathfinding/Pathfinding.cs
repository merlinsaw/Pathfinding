using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarPathfinding
{
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
	  FindPath(StartPosition.position, TargetPosition.position);
	}

	private void Cashing()
	{
	  GridReference = GetComponent<Grid>();
	}

	void FindPath(Vector3 starPosition, Vector3 targetPosition)
	{
	  Node StartNode = GridReference.NodeFromWorldPosition(starPosition);
	  Node TargetNode = GridReference.NodeFromWorldPosition(targetPosition);

	  List<Node> OpenList = new List<Node>();
	  HashSet<Node> ClosedList = new HashSet<Node>();//TODO: why is a hashset used here? maybe because we use ClosedList.Containes aftewards

	  OpenList.Add(StartNode);

	  while (OpenList.Count > 0)
	  {
		Node CurrentNode = OpenList[0];
		for (int node = 1; node < OpenList.Count; node++)
		{
		  if (OpenList[node].FCost < CurrentNode.FCost || OpenList[node].FCost == CurrentNode.FCost && OpenList[node].HCost < CurrentNode.HCost)
		  {
			CurrentNode = OpenList[node];
		  }
		}
		OpenList.Remove(CurrentNode);
		ClosedList.Add(CurrentNode);

		if (CurrentNode == TargetNode)
		{
		  GetFinalPath(StartNode, TargetNode);
		}

		foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))
		{
		  if (!NeighborNode.IsWall || ClosedList.Contains(NeighborNode))
		  {
			continue;
		  }
		  int MoveCost = CurrentNode.GCost + GetManhattenDistance(CurrentNode, NeighborNode);

		  if (MoveCost < NeighborNode.GCost || !OpenList.Contains(NeighborNode))
		  {
			NeighborNode.GCost = MoveCost;
			NeighborNode.HCost = GetManhattenDistance(NeighborNode, TargetNode);
			NeighborNode.Parent = CurrentNode; // TODO: verstehe ich nicht ganz
		  }

		  if (!OpenList.Contains(NeighborNode))
		  {
			OpenList.Add(NeighborNode);
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