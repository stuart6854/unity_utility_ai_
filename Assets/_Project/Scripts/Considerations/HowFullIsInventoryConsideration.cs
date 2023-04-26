using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "HowFullIsInventory", menuName = "UtilityAI/Consideration/How Full Is Inventory")]
    public class HowFullIsInventoryConsideration : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            Score = responseCurve.Evaluate(Mathf.Clamp01(npc.Inventory.HowFullIsInventory()));
            return Score;
        }
    }
}