using UnityEngine;
using UnityEngine.AI;

namespace SM
{
    public class MoveController : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MoveTo(Vector3 position)
        {
            _agent.destination = position;
        }
    }
}