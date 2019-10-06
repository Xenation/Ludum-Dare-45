using UnityEngine;

namespace LD45 {
	public enum Effect {
		Upgrade,
		Portal
	}

	public class EffectManager : Singleton<EffectManager> {

		public GameObject upgradeEffect;
		public GameObject portalEffect;

		public void PlayEffect(Effect effect, Transform tr) {
			GameObject prefab = null;
			switch (effect) {
				case Effect.Upgrade:
					prefab = upgradeEffect;
					break;
				case Effect.Portal:
					prefab = portalEffect;
					break;
			}
			Instantiate(prefab, tr.position, tr.rotation);
		}

	}
}
