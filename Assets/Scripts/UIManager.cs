using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CCGames
{
	public class UIManager : SingletonMonoBehaviour<UIManager>
	{
		[SerializeField]
		private HitCountText _hitCountText;

		[SerializeField]
		private ETCJoystick _joystick;

		[SerializeField]
		private ETCButton _attackButton;

		public HitCountText HitCountText => _hitCountText;

		public void AddOnMoveCallback(UnityAction<Vector2> onMove)
		{
			_joystick.onMove.RemoveAllListeners();
			_joystick.onMove.AddListener(onMove);
		}

		public void AddOnAttackCallback(UnityAction onAttack)
		{
			_attackButton.onDown.RemoveAllListeners();
			_attackButton.onDown.AddListener(onAttack);
		}
	}
}