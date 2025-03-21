using UnityEngine;
using UnityEngine.AI;

namespace Core.Units
{
    public class UnitNavmeshMovement
    {
        private readonly NavMeshAgent _agent;
        private Vector3 _destination;

        public UnitNavmeshMovement(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public void ChangeDestination(Vector3 position)
        {
            _agent.destination = _destination;
        }
    }
}