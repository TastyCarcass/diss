using System;
using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]



    public class AICharacterControl : MonoBehaviour
	{
		public float normSpeed = 3.5f;
		public float speedMultiplier = 1.0f;

        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target; // target to aim for

		private bool initialised = false;
		//Added for project
		public int nodeID;

		public void OnCollisionEnter()
		{
			Debug.LogError ("We have entered");
		}

		public void Entered()
		{
			speedMultiplier = 0.5f;
		}

		public void Exited()
		{
			speedMultiplier = 1.0f;
		}
        // Use this for initialization
        private void Start()
        {
			if (!initialised)
			{
	            // get the components on the object we need ( should not be null due to require component so no need to check )
	            agent = GetComponentInChildren<NavMeshAgent>();
	            character = GetComponent<ThirdPersonCharacter>();

		        agent.updateRotation = false;
		        agent.updatePosition = true;
			}
        }


        // Update is called once per frame
        private void Update()
        {
			if (!initialised)
			{
				Start ();
			}

            if (target != null)
            {
				//Added for project: Stop at target
				float distance = Vector3.Distance(character.transform.position, target.transform.position);

				if(distance < 1f)
				{
					agent.SetDestination(target.position);
					character.Move(Vector3.zero, false, false);
				}
				else
				{
					agent.speed = normSpeed * speedMultiplier;
                	agent.SetDestination(target.position);
				
                	// use the values to move the character
                	character.Move(agent.desiredVelocity, false, false);
				}
            }
            else
            {
                // We still need to call the character's move function, but we send zeroed input as the move param.
                character.Move(Vector3.zero, false, false);
            }

        }
		

		public void SetNodeTarget (Transform _targetNode, int nodeId)
		{
			target = _targetNode;
			nodeID = nodeId;
		}
    }
}
