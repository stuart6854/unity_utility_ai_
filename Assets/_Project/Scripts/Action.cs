using UnityEngine;

namespace SM
{
    public abstract class Action : ScriptableObject
    {
        public string Name;

        public float Score
        {
            get => _score;
            set => _score = Mathf.Clamp01(value);
        }

        public Consideration[] Considerations;

        public Transform RequiredDestination { get; protected set; }

        private float _score;


        protected virtual void Awake()
        {
            Score = 0;
        }

        public abstract void Execute(NPCController npc);

        public abstract void SetRequiredDestination(NPCController npc);
    }
}