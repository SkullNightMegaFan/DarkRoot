using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;
		//private float enemySearch = 3.0f;

		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			if (target != null && ai != null) 
			{
				//Debug.Log("We got one");
				// if (enemySearch <= 0.0f)
				// {

				// }
				ai.destination = target.position;
				//StartCoroutine(OneSec());
				return;
				//target = null;
			//if target is within range of collider and ai is not null then 
			//ai.destination = target.position for 3 seconds
			//return;
			//ontriggerStay2D if tag == "player"
			//ai.destination == other.position
			//on triggerenterexit2D if tag == "player"
			//ai.position = other.position.

			}

		}
		IEnumerator OneSec()
		{
			yield return new WaitForSeconds(1.0f);
							Debug.Log("One Second has passed");

		}
	}
}
