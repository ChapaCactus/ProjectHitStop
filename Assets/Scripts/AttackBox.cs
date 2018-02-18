using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CCGames
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class AttackBox : MonoBehaviour
	{
		[SerializeField]
		private CircleCollider2D _attackRange;

		public int Power { get; private set; } = 0;
		public string TargetTag { get; private set; } = "";

		private static readonly string TargetTagFooter = "HitBox";

		public void Setup(bool isPlayer, int power)
		{
			SetTargetTagName(isPlayer);

			Power = power;
		}

		public void OnTriggerEnter2D(Collider2D collision)
		{
			if (!collision.CompareTag(TargetTag)) return;

			var hitBox = collision.GetComponent<HitBox>();
			hitBox.Hit(Power);
		}

		public void SetEnable(bool enable) => _attackRange.enabled = enable;

		private void SetTargetTagName(bool isPlayer)
		{
			var tagHeader = isPlayer ? "Enemy" : "Player";
			TargetTag = $"{tagHeader}_{TargetTagFooter}";
		}
	}
}