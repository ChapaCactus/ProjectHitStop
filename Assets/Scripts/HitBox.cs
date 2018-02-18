using System;

using UnityEngine;

namespace CCGames
{
	public class HitBox : MonoBehaviour
	{
		[SerializeField]
		private CircleCollider2D _hitbox;

		private Action<int> _onHit;

		public void Setup(bool isPlayer, Action<int> onHit)
		{
			SetTag(isPlayer);

			_onHit = onHit;
		}

		public void Hit(int damage)
		{
			_onHit.SafeCall(damage);
		}

		private void SetTag(bool isPlayer)
		{
			var tagHeader = isPlayer ? "Player" : "Enemy";
			tag = $"{tagHeader}_HitBox";
		}
	}
}