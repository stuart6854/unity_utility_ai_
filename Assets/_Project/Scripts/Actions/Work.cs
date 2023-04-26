using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "Work", menuName = "UtilityAI/Actions/Work")]
    public class Work : Action
    {
        public override void Execute(NPCController npc)
        {
            npc.DoWork(3);
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            float nearestDistance = Mathf.Infinity;
            Transform nearestResource = null;

            var resources = npc.Context.Destinations[DestinationType.Resource];
            foreach (var resource in resources)
            {
                float dist = Vector3.Distance(resource.position, npc.transform.position);
                if (dist < nearestDistance)
                {
                    nearestResource = resource;
                    nearestDistance = dist;
                }
            }

            RequiredDestination = nearestResource;
        }
    }
}