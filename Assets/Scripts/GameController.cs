using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CCGames
{
	public class GameController : SingletonMonoBehaviour<GameController>
	{
		public CharacterController Player { get; private set; }

		public List<CharacterController> Enemies { get; private set; }

		private void Awake()
		{
			// test
			SetupStage(StageID.Stage01);
		}

		public void CreatePlayer()
		{
			Player?.Kill();
			CharacterController.CreatePlayer(FieldController.I.transform, SetPlayer);
			UIManager.I.AddOnMoveCallback(Player.OnMove);
			UIManager.I.AddOnAttackCallback(Player.OnStartAttack);
		}

		public void CreateEnemy(Vector2 startPos)
		{
			var model = CharacterModel.CreateEnemyData(EnemyID.Enemy01);
			CharacterController.Create(model, FieldController.I.transform, startPos, SetEnemy);
		}

		private void SetupStage(StageID stageID)
		{
			Enemies = new List<CharacterController>();

			CreatePlayer();
		}

		private void SetPlayer(CharacterController player)
		{
			Player = player;
		}

		private void SetEnemy(CharacterController enemy)
		{
			Enemies.Add(enemy);
		}
	}
}