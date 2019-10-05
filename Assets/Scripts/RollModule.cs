using UnityEngine;

namespace LD45 {
	public class RollModule : CubeModule {

		public float walkForce = 2f;
		public float rotateForce = 10f;
		
		private Vector3 torqueVec = Vector3.zero;
		private Vector2 rollDirection = Vector2.zero;

		private void Update() {
			torqueVec = Vector3.zero;
			rollDirection = Vector2.zero;

			if (Input.GetKey(KeyCode.Z)) {
				rollDirection += Vector2.up;
				torqueVec += Vector3.right;
			}
			if (Input.GetKey(KeyCode.S)) {
				rollDirection -= Vector2.up;
				torqueVec -= Vector3.right;
			}
			if (Input.GetKey(KeyCode.Q)) {
				rollDirection -= Vector2.right;
				torqueVec += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.D)) {
				rollDirection += Vector2.right;
				torqueVec -= Vector3.forward;
			}
			rollDirection.Normalize();
			torqueVec.Normalize();
			torqueVec *= walkForce;

			if (Input.GetKey(KeyCode.E)) {
				torqueVec += Vector3.up * rotateForce;
			}
			if (Input.GetKey(KeyCode.A)) {
				torqueVec -= Vector3.up * rotateForce;
			}
		}

		private void FixedUpdate() {
			cube.rb.AddTorque(torqueVec);
		}

		public Vector2 GetRollDirection() {
			return rollDirection;
		}

	}
}
