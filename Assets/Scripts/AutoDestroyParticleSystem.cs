using UnityEngine;

namespace LD45 {
	[RequireComponent(typeof(ParticleSystem))]
	public class AutoDestroyParticleSystem : MonoBehaviour {

		private ParticleSystem ps;

		private void Start() {
			ps = GetComponent<ParticleSystem>();
		}

		private void Update() {
			if (ps.isStopped) {
				Destroy(gameObject);
			}
		}

	}
}
