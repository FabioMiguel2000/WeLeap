using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Leap.Unity.Interaction.PhysicsHands.Example
{
    public class PlayerRecenter : MonoBehaviour
    {
        [SerializeField] private GameObject anchor;
        [SerializeField] private GameObject player;
        private InputManager inputManager;

        public void Start()
        {
            inputManager = InputManager.Instance;
        }

        private void Update()
        {
            // if (inputManager.GetTriggerWasReleasedThisFrame())
            // {
            //     Debug.Log("Released!");
            //     Recenter();
            // }
        }

        public void Recenter()
        {
            player.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
            return;
        }
    }
}