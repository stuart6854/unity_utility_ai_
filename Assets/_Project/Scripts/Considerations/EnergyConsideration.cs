using UnityEngine;

namespace SM
{
    [CreateAssetMenu(fileName = "Energy", menuName = "UtilityAI/Consideration/Energy")]
    public class EnergyConsideration : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            Score = responseCurve.Evaluate(Mathf.Clamp01(npc.Stats.Energy / 100.0f));
            return Score;
        }
    }
}