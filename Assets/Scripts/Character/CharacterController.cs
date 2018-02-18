using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CCGames
{
	[RequireComponent(typeof(CharacterView))]
	public class CharacterController : MonoBehaviour, IHitStop
	{
		[SerializeField]
		private AttackBox _attackBox;

		[SerializeField]
		private HitBox _hitbox;

		[SerializeField]
		private Rigidbody2D _rigid2D;

		private Tween _moveTween = null;

		public bool IsPlayable => _model.IsPlayable;
		public bool IsHitStopping => _model.IsHitStopping;
		public bool IsAttacking => _model.IsAttacking;

		private CharacterModel _model { get; set; }
		private CharacterView _view { get; set; }

		private Vector2 _moveDirection { get; set; } = Vector2.zero;

		public static void CreatePlayer(Transform parent, Action<CharacterController> onCreate)
		{
			var model = CharacterModel.CreatePlayerData();

			Create(model, parent, Vector2.zero, onCreate);
		}

		public static void Create(CharacterModel model, Transform parent, Vector2 startPos, Action<CharacterController> onCreate)
		{
			var prefab = Resources.Load(model.PrefabPath) as GameObject;
			var controller = Instantiate(prefab, parent).GetComponent<CharacterController>();

			if (model.IsPlayable)
			{
				controller.name = "Player";
				controller.tag = "Player";
			}
			else
			{
				controller.name = "Enemy";
				controller.tag = "Enemy";
			}

			controller.Setup(model);
			controller.transform.localPosition = startPos;

			onCreate.SafeCall(controller);
		}

		public void Setup(CharacterModel model)
		{
			_model = model;
			_view = GetComponent<CharacterView>();

			_attackBox.SetEnable(false);

			_attackBox.Setup(IsPlayable, 100);
			_hitbox.Setup(IsPlayable, OnHit);

			HitStopManager.I.Subscribe(this);
		}

		public void OnHit(int damage)
		{
			HitStopManager.I.CallStartHitStop();
			GameManager.AddCombo();

			Damage(damage);
		}

		public void OnMove(Vector2 move)
		{
			if (IsAttacking) return;
			if (FieldController.I.IsMoving) return;

			transform.localPosition += new Vector3(move.x * 5, move.y * 5, 0);
			_moveDirection = move;
		}

		public void OnStartAttack()
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

		public void OnEndAttack()
		{
			_model.IsAttacking = false;

			_attackBox.SetEnable(false);
		}

		public void Stop()
		{
			_model.IsHitStopping = true;

			_moveTween.Pause();
		}

		public void Eject()
		{
			_model.IsHitStopping = false;

			_moveTween.Play();
		}

		public void Damage(int damage)
		{
			// 攻撃中ならダメージ無視
			if (IsAttacking) return;

			_model.Damage(damage, Kill);
		}

		public void Kill()
		{
			HitStopManager.I.RemoveSubscribe(this);

			Destroy(gameObject);
		}

		private void Reset()
		{
			_model.IsHitStopping = false;
		}
	}
}