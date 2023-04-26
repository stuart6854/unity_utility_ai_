using System;
using TMPro;
using UnityEngine;

namespace SM
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bestActionText;
        [SerializeField] private TextMeshProUGUI statsText;
        [SerializeField] private TextMeshProUGUI inventoryText;
        private Transform _cameraTransform;

        private void Awake()
        {
            if (Camera.main != null)
                _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward,
                _cameraTransform.rotation * Vector3.up);
        }

        public void UpdateBestActionText(string bestAction)
        {
            bestActionText.text = $"Best Action: {bestAction}";
        }

        public void UpdateStatsText(int energy, int hunger, int money)
        {
            statsText.text = $"Energy: {energy}\nHunger: {hunger}\nMoney: {money}";
        }

        public void UpdateInventoryText(int wood, int stone, int food)
        {
            inventoryText.text = $"Wood: {wood}\nStone: {stone}\nFood: {food}";
        }
    }
}