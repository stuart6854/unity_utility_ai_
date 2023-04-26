using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
    public class Sleep : Action
    {
        public override void Execute(NPCController npc)
        {
            npc.DoSleep(3);
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            RequiredDestination = npc.Context.Home.transform;
        }
    }
}