using UnityEngine;

namespace LD45 {
	public class CubeMover : MonoBehaviour {

		public static readonly Vector3[] corners = {
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, -0.5f)
		};

		public float walkForce = 2f;
		public float rotateForce = 10f;
		public float jumpForce = 10f;
		public float killY = -5f;

		public Material revealMat;
		public float revealRadius = 2f;

		private Rigidbody rb;
		private Vector3 torqueVec = Vector3.zero;

		private void Start() {
			rb = GetComponent<Rigidbody>();
			revealMat.SetFloat("_RevealRadius", revealRadius);
		}

		private void Update() {
			revealMat.SetVector("_RevealCenter", transform.position);

			if (isOnGround()) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				}
			}

            if (Input.GetKeyDown(KeyCode.R))
            {
                Level.current.ReloadLevel();
            }

			torqueVec = Vector3.zero;

			if (Input.GetKey(KeyCode.Z)) {
				torqueVec += Vector3.right;
			}
			if (Input.GetKey(KeyCode.S)) {
				torqueVec -= Vector3.right;
			}
			if (Input.GetKey(KeyCode.Q)) {
				torqueVec += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.D)) {
				torqueVec -= Vector3.forward;
			}
			torqueVec.Normalize();
			torqueVec *= walkForce;

			if (Input.GetKey(KeyCode.E)) {
				torqueVec += Vector3.up * rotateForce;
			}
			if (Input.GetKey(KeyCode.A)) {
				torqueVec -= Vector3.up * rotateForce;
			}

			if (transform.position.y < killY) {
				Level.current.ReloadLevel();
			}
		}

		private void FixedUpdate() {
			rb.AddTorque(torqueVec);
		}

		private void OnDestroy() {
			revealMat.SetFloat("_RevealRadius", 1000f);
		}

		public bool isOnGround() {
			for (int i = 0; i < 8; i++) {
				if (Physics.CheckBox(transform.TransformPoint(corners[i]), Vector3.one * 0.05f, transform.rotation, LayerMask.GetMask("Ground"))) {
					return true;
				}
			}
			return false;
		}

	}
}
