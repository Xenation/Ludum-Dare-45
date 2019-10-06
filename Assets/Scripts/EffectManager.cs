using UnityEngine;

namespace LD45 {
	public enum Effect {
		Upgrade,
		Portal,
		Destructible
	}

	public class EffectManager : Singleton<EffectManager> {

		public GameObject upgradeEffect;
		public GameObject portalEffect;
		public GameObject destructibleEffect;

		public void PlayEffect(Effect effect, Transform tr) {
			GameObject prefab = null;
			switch (effect) {
				case Effect.Upgrade:
					prefab = upgradeEffect;
					break;
				case Effect.Portal:
					prefab = portalEffect;
					break;
				case Effect.Destructible:
					prefab = destructibleEffect;
					break;
			}
			GameObject effectObj = Instantiate(prefab, tr.position, tr.rotation);
			if (effect == Effect.Destructible) {
				effectObj.transform.localScale = tr.localScale;
			}
		}

	}
}
