using UnityEngine;

namespace LD45 {
	public class CubeMover : MonoBehaviour {

		public float walkForce = 2f;
		public float rotateForce = 10f;
		public float jumpForce = 10f;

		private Rigidbody rb;

		private void Start() {
			rb = GetComponent<Rigidbody>();
		}

		private void Update() {
			Ray groundRay = new Ray(transform.position, Vector3.down);
			RaycastHit hit;
			if (Physics.Raycast(groundRay, out hit, .6f, LayerMask.GetMask("Ground"))) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				}
			}


			Vector3 forceVec = Vector3.zero;

			if (Input.GetKey(KeyCode.Z)) {
				forceVec += Vector3.right * walkForce;
			}
			if (Input.GetKey(KeyCode.S)) {
				forceVec -= Vector3.right * walkForce;
			}
			if (Input.GetKey(KeyCode.Q)) {
				forceVec += Vector3.forward * walkForce;
			}
			if (Input.GetKey(KeyCode.D)) {
				forceVec -= Vector3.forward * walkForce;
			}
			if (Input.GetKey(KeyCode.E)) {
				forceVec += Vector3.up * rotateForce;
			}
			if (Input.GetKey(KeyCode.A)) {
				forceVec -= Vector3.up * rotateForce;
			}

			//rb.AddForce(forceVec);
			rb.AddTorque(forceVec);
		}

	}
}
