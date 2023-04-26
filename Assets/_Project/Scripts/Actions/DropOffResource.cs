using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "DropOfResource", menuName = "UtilityAI/Actions/Drop Of Resource")]
    public class DropOfResource : Action
    {
        public override void Execute(NPCController npc)
        {
            Debug.Log("Dropped off resource.");
            
            npc.Inventory.RemoveAllResources();
            npc.Stats.Money += 20;
            npc.AiBrain.FinishedExecutingBestAction = true;
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            RequiredDestination = npc.Context.Storage.transform;
        }
    }
}