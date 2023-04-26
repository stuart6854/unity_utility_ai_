using UnityEngine;

namespace SM
{
    
    [CreateAssetMenu(fileName = "Money", menuName = "UtilityAI/Consideration/Money")]
    public class MoneyConsideration : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            Score = responseCurve.Evaluate(Mathf.Clamp01(npc.Stats.Money / 1000.0f));
            return Score;
        }
    }
}