using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;
using CCGames.Constants;

namespace CCGames
{
	public class FieldController : SingletonMonoBehaviour<FieldController>
	{
		public Vector2 MapOriginPos = new Vector2(0, 280);
		public Vector2 CurrentMapAddress { get; private set; } = Vector2.zero;

		public bool IsMoving { get; private set; } = false;

		private Dictionary<Vector2, GroundController> _grounds { get; set; } = new Dictionary<Vector2, GroundController>();

		private void Awake()
		{
			transform.position = MapOriginPos;
		}

		private void Start()
		{
			GetGround(CurrentMapAddress, current => current.OnEnter());
		}

		public void RegistNewGround(GroundController ground)
		{
			if (_grounds.ContainsKey(ground.GetMapAddress)) return;

			_grounds.Add(ground.GetMapAddress, ground);
		}

		public void Move(Directions dir)
		{
			if (IsMoving) return;

			IsMoving = true;

			GetGround(CurrentMapAddress, before => before.OnExit());

			var moveVec = Vector2.up;
			switch (dir)
			{
				case Directions.Up:
					moveVec = Vector2.down;
					CurrentMapAddress += Vector2.up;
					break;
				case Directions.Right:
					moveVec = Vector2.left;
					CurrentMapAddress += Vector2.right;
					break;
				case Directions.Down:
					moveVec = Vector2.up;
					CurrentMapAddress += Vector2.down;
					break;
				case Directions.Left:
					moveVec = Vector2.right;
					CurrentMapAddress += Vector2.left;
					break;
			}

			transform.DOLocalMove(moveVec * 720, 2)
					 .SetRelative()
			         .OnComplete(() =>
					 {
						 GetGround(CurrentMapAddress, next => next.OnEnter());
						 IsMoving = false;
					 });
		}

		private void GetGround(Vector2 mapAddress, Action<GroundController> callback)
		{
			if (!_grounds.ContainsKey(mapAddress)) return;

			callback.SafeCall(_grounds[mapAddress]);
		}
	}
}