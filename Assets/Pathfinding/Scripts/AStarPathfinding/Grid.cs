using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AStarPathfinding
{
  public class Grid : MonoBehaviour
  {
	public bool onlyDisplayPathGismos;
	public Transform StartPosition;
	public LayerMask WallMask;
	public Vector2 GridWorldSize;
	public float NodeRadius;
	public float DistanceBetweenNodes;

	Node[,] NodeArray;
	public List<Node> FinalPath;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	private void Start()
	{
	  nodeDiameter = NodeRadius * 2;
	  gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
	  gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
	  CreateGrid();
	}

	public int MaxSize
	{
	  get
	  {
		return gridSizeX * gridSizeY;
	  }
	}

	public Node NodeFromWorldPosition(Vector3 worldPosition)
	{
	  float xpoint = ((worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x);
	  float ypoint = ((worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y);

	  xpoint = Mathf.Clamp01(xpoint);
	  ypoint = Mathf.Clamp01(ypoint);

	  int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
	  int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

	  return NodeArray[x, y];
	}

	void CreateGrid()
	{
	  NodeArray = new Node[gridSizeX, gridSizeY];
	  Vector3 bottomLeft = transform.position - Vector3.right * GridWorldSize.x
		/ 2 - Vector3.forward * GridWorldSize.y / 2;
	  for (int x = 0; x < gridSizeX; x++)
	  {
		for (int y = 0; y < gridSizeY; y++)
		{
		  Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
		  bool Wall = true;

		  if (Physics.CheckSphere(worldPoint, NodeRadius, WallMask))
		  {
			Wall = false;
		  }

		  NodeArray[x, y] = new Node(Wall, worldPoint, x, y);
		}
	  }
	}

	public List<Node> GetNeighboringNodes(Node node)
	{
	  List<Node> NeighborList = new List<Node>();
	  int xCheck;
	  int yCheck;

	  //Right Side
	  xCheck = node.GridX + 1;
	  yCheck = node.GridY;
	  if (xCheck >= 0 && xCheck < gridSizeX)
	  {
		if (yCheck >= 0 && yCheck < gridSizeY)
		{
		  NeighborList.Add(NodeArray[xCheck, yCheck]);
		}
	  }
	  //Left Side
	  xCheck = node.GridX - 1;
	  yCheck = node.GridY;
	  if (xCheck >= 0 && xCheck < gridSizeX)
	  {
		if (yCheck >= 0 && yCheck < gridSizeY)
		{
		  NeighborList.Add(NodeArray[xCheck, yCheck]);
		}
	  }
	  //Top Side
	  xCheck = node.GridX;
	  yCheck = node.GridY + 1;
	  if (xCheck >= 0 && xCheck < gridSizeX)
	  {
		if (yCheck >= 0 && yCheck < gridSizeY)
		{
		  NeighborList.Add(NodeArray[xCheck, yCheck]);
		}
	  }
	  //Bottom Side
	  xCheck = node.GridX;
	  yCheck = node.GridY - 1;
	  if (xCheck >= 0 && xCheck < gridSizeX)
	  {
		if (yCheck >= 0 && yCheck < gridSizeY)
		{
		  NeighborList.Add(NodeArray[xCheck, yCheck]);
		}
	  }
	  return NeighborList;
	}

	//Method to Draw a colored gizmo wireframe grid.
	private void OnDrawGizmos()
	{
	  Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

	  if (onlyDisplayPathGismos)
	  {
		if (FinalPath != null)
		{
		  foreach (Node node in FinalPath)
		  {
			Gizmos.color = Color.red;
			Gizmos.DrawCube(node.Position, new Vector3(1, 0.25f, 1) * (nodeDiameter - DistanceBetweenNodes));
		  }
		}
	  }
	  else
	  {
		if (NodeArray != null)
		{
		  foreach (Node node in NodeArray)
		  {
			if (node.IsWall)
			{
			  Gizmos.color = Color.white;
			}
			else
			{
			  Gizmos.color = Color.yellow;
			}

			if (FinalPath != null)
			{
			  if (FinalPath.Contains(node))
			  {
				Gizmos.color = Color.red;
			  }
			}

			Gizmos.DrawCube(node.Position, new Vector3(1, 0.25f, 1) * (nodeDiameter - DistanceBetweenNodes));
		  }
		}
	  }
	}

  }
}