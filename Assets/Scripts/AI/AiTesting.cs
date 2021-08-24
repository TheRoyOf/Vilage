using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiTesting : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent = null;

    public void SetTargetDestination(Vector2 point)
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(point), out hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
