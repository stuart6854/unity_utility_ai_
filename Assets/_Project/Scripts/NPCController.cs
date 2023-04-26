using System.Collections;
using UnityEngine;

namespace SM
{
    public enum State
    {
        Decide,
        Move,
        Execute,
    }

    public class NPCController : MonoBehaviour
    {
        public MoveController Mover { get; set; }
        public AIBrain AiBrain { get; set; }
        public NPCInventory Inventory { get; set; }
        public Stats Stats { get; set; }

        public Context Context;

        private State _currentState;

        private void Awake()
        {
            Mover = GetComponent<MoveController>();
            AiBrain = GetComponent<AIBrain>();
            Inventory = GetComponent<NPCInventory>();
            Stats = GetComponent<Stats>();

            _currentState = State.Decide;
        }

        private void Update()
        {
            // if (AiBrain.FinishedDeciding)
            // {
            //     AiBrain.FinishedDeciding = false;
            //     AiBrain.BestAction.Execute(this);
            // }

            FSMTick();
            
            Stats.UpdateEnergy(AmIAtRestDestination());
            Stats.UpdateHunger();
        }

        private void FSMTick()
        {
            if (_currentState == State.Decide)
            {
                AiBrain.DecideBestAction();
                _currentState =
                    Vector3.Distance(AiBrain.BestAction.RequiredDestination.position, this.transform.position) < 2.0f
                        ? State.Execute
                        : State.Move;
            }
            else if (_currentState == State.Move)
            {
                if (Vector3.Distance(AiBrain.BestAction.RequiredDestination.position, this.transform.position) < 2.0f)
                    _currentState = State.Execute;
                else
                {
                    Mover.MoveTo(AiBrain.BestAction.RequiredDestination.position);
                }
            }
            else if (_currentState == State.Execute)
            {
                if (!AiBrain.FinishedExecutingBestAction)
                    AiBrain.BestAction.Execute(this);
                else if (AiBrain.FinishedExecutingBestAction)
                    _currentState = State.Decide;
            }
        }

        #region Workhorse Methods

        public bool AmIAtRestDestination()
        {
            return Vector3.Distance(this.transform.position, Context.Home.transform.position) <= Context.MinDistance;
        }

        #endregion

        #region Coroutines

        public void DoWork(int time)
        {
            StartCoroutine(WorkCoroutine(time));
        }

        private IEnumerator WorkCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1.0f);
                --counter;
            }

            Debug.Log("I am working!");
            // Logic to update things involved with work
            Inventory.AddResource(ResourceType.Wood, 100);

            // Decide out new best action after you finished one
            AiBrain.FinishedExecutingBestAction = true;
        }

        public void DoSleep(int time)
        {
            StartCoroutine(SleepCoroutine(time));
        }

        private IEnumerator SleepCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1.0f);
                --counter;
            }

            Debug.Log("I just slept and recovered 1 energy!");
            // Logic to update energy
            Stats.Energy += 1;

            AiBrain.FinishedExecutingBestAction = true;
        }

        #endregion
    }
}