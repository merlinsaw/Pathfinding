//-------------------------------------------------------
//	
//-------------------------------------------------------

#region using

using UnityEngine;
using UnityEngine.AI;

#endregion

namespace NavmeshPathfinding
{
  public class MoveTarget : MonoBehaviour
  {
	public NavMeshAgent NavAgent;
	private void Update()
	{
	  if (Input.GetMouseButtonDown(0))//left klicked
	  {
		Vector3 mouse = Input.mousePosition;
		Ray castPoint = Camera.main.ScreenPointToRay(mouse);
		RaycastHit hit;
		if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
		{
		  NavAgent.SetDestination(hit.point);
		}
	  }
	}

  }
}
