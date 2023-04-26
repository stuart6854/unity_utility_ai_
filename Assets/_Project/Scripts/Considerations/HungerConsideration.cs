using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "Hunger", menuName = "UtilityAI/Consideration/Hunger")]
    public class HungerConsideration : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            Score = responseCurve.Evaluate(Mathf.Clamp01(npc.Stats.Hunger / 100.0f));
            return Score;
        }
    }
}