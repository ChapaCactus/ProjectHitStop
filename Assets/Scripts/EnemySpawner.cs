using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CCGames
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField]
		private EnemyID[] _spawnEnemyIDs = new EnemyID[0];

		public void RequestInstantiate(Transform parent, Action<CharacterController> onCreateSuccess)
		{
			if (_spawnEnemyIDs.Length == 0) return;

			var id = _spawnEnemyIDs[0];
			Instantiate(id, parent, onCreateSuccess);
		}

		private void Instantiate(EnemyID id, Transform parent, Action<CharacterController> onCreateSuccess)
		{
			var model = CharacterModel.CreateEnemyData(id);
			CharacterController.Create(model, parent, transform.localPosition, onCreateSuccess.SafeCall);
		}
	}
}