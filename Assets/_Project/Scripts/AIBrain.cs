using UnityEngine;

namespace SM
{
    public class AIBrain : MonoBehaviour
    {
        
        public Action BestAction { get; set; }
        public bool FinishedDeciding { get; set; }
        public bool FinishedExecutingBestAction { get; set; }

        [SerializeField] private Action[] actionsAvailable;
        [SerializeField] private Billboard billboard;
        
        private NPCController _npc;

        private void Awake()
        {
            _npc = GetComponent<NPCController>();
            FinishedDeciding = false;
            FinishedExecutingBestAction = false;
        }
        
        /*
         * Get the highest scoring action.
         */
        public void DecideBestAction()
        {
            FinishedExecutingBestAction = false;
            
            float bestActionScore = 0.0f;
            int bestActionIndex = 0;
            for (int i = 0; i < actionsAvailable.Length; i++)
            {
                var actionScore = ScoreAction(actionsAvailable[i]);
                if (actionScore > bestActionScore)
                {
                    bestActionIndex = i;
                    bestActionScore = actionScore;
                }
            }

            BestAction = actionsAvailable[bestActionIndex];
            BestAction.SetRequiredDestination(_npc);
            FinishedDeciding = true;
            
            billboard.UpdateBestActionText(BestAction.Name);
        }

        /*
         * Score all considerations. Aggregate (somehow) consideration score to get overall action score.
         */
        private float ScoreAction(Action action)
        {
            float score = 1.0f;
            foreach (var consideration in action.Considerations)
            {
                float considerationScore = consideration.ScoreConsideration(_npc);
                score *= considerationScore;

                if (score == 0.0f)
                {
                    action.Score = 0.0f;
                    return action.Score; // No point continuing
                }
            }
            
            // Averaging scheme of overall scheme
            float originalScore = score;
            float modFactor = 1.0f - (1.0f / action.Considerations.Length);
            float makeupValue = (1.0f - originalScore) * modFactor;
            action.Score = originalScore + (makeupValue * originalScore);
            
            return action.Score;
        }
    }
}