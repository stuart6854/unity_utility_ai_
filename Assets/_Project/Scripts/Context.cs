using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace SM
{
    public class Context : MonoBehaviour
    {
        public Storage Storage;
        public GameObject Home;
        [FormerlySerializedAs("ResourceTage")] public string ResourceTag = "resource";
        public float MinDistance = 5.0f;
        public Dictionary<DestinationType, List<Transform>> Destinations {get; private set; }

        private void Awake()
        {
            var restDestinations = new List<Transform> { Home.transform };
            var storageDestinations = new List<Transform> { Storage.transform };
            var resourceDestinations = GetAllResources();

            Destinations = new Dictionary<DestinationType, List<Transform>>()
            {
                { DestinationType.Rest, resourceDestinations },
                { DestinationType.Storage, storageDestinations },
                { DestinationType.Resource, resourceDestinations },
            };
        }

        private List<Transform> GetAllResources()
        {
            var gameObjects = FindObjectsOfType<Transform>();
            return gameObjects.Where(go => go.gameObject.CompareTag(ResourceTag)).ToList();
        }
    }
}