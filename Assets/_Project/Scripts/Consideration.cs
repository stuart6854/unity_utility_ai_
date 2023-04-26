using UnityEngine;

namespace SM
{
    public abstract class Consideration : ScriptableObject
    {
        public string Name;

        public float Score
        {
            get => _score;
            set => _score = Mathf.Clamp01(value);
        }
        
        private float _score;

        protected virtual void Awake()
        {
            Score = 0;
        }

        public abstract float ScoreConsideration(NPCController npc);
    }
}