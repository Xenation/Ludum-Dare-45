using UnityEngine;

namespace LD45 {
	public class LevelEnd : MonoBehaviour {

		private void OnTriggerEnter(Collider other) {
			if (((1 << other.gameObject.layer) & LayerMask.GetMask("PlayerWhite", "PlayerBlack")) == 0) return;

			other.attachedRigidbody.AddForce(transform.TransformVector(Vector3.forward).normalized * 80f, ForceMode.Impulse);
			EffectManager.I.PlayEffect(Effect.Portal, transform.parent.Find("Center"));
			Level.current.TriggerNextLevel();
		}

	}
}
