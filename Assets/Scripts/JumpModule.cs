using UnityEngine;

namespace LD45 {
	public class JumpModule : CubeModule {

		public float jumpForce = 10f;
		public float horizontalWeight = 0.25f;
		
		private void Update() {
			Vector3 contactPoint;
			if (cube.GroundContact(out contactPoint)) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					Vector3 dir = Vector3.up;
					dir += GetComponent<RollModule>().GetRollDirection().Unflat(0f) * horizontalWeight;
					dir.Normalize();
					cube.rb.AddForce(dir * jumpForce, ForceMode.Impulse);
				}
			}
		}

	}
}
