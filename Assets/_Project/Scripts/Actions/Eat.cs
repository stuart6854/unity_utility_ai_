using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
    public class Eat : Action
    {
        public override void Execute(NPCController npc)
        {
            Debug.Log("I ate food!");
            
            // Logic for updating everything involved with eating
            npc.Stats.Hunger -= 30;
            npc.Stats.Money -= 10;
            
            npc.AiBrain.FinishedExecutingBestAction = true;
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            RequiredDestination = null;
        }
    }
}