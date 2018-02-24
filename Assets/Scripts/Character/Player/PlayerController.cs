using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CCGames
{
	public class PlayerController : CharacterController
	{
		public static void Create(Transform parent, Action<PlayerController> onCreate)
		{
			var model = CharacterModel.CreatePlayerData();

			Create(model, parent, Vector2.zero, player => onCreate.SafeCall(player as PlayerController));
		}

		public override void OnStartAttack()
		{
			_model.IsAttacking = true;
			_attackBox.SetEnable(true);

			var moveX = 0;
			if (_moveDirection.x < 0) moveX = -1;
			if (_moveDirection.x > 0) moveX = 1;
			var moveY = 0;
			if (_moveDirection.y < 0) moveY = -1;
			if (_moveDirection.y > 0) moveY = 1;

			_moveTween = transform.DOLocalMove(new Vector2(moveX, moveY) * 128, 0.1f)
					 .SetEase(Ease.Linear)
					 .SetRelative()
					 .OnComplete(OnEndAttack);
		}
	}
}