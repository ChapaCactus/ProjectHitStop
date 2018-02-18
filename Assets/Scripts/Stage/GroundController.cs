using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CCGames
{
	public class GroundController : MonoBehaviour
	{
		[SerializeField]
		private Vector2 _mapAddress = Vector2.zero;

		[SerializeField]
		private SpriteRenderer _renderer;

		public Vector2 GetMapAddress => _mapAddress;

		public List<CharacterController> _enemies { get; private set; } = new List<CharacterController>();

		private void Awake()
		{
			Setup(_mapAddress);

			OnExit();
		}

		public void Setup(Vector2 mapAddress)
		{
			FieldController.I.RegistNewGround(this);
		}

		public void OnEnter()
		{
			var exitAreas = GetComponentsInChildren<ExitArea>().ToList();
			exitAreas.ForEach(area => area.SetEnable(true));

			CreateEnemies(_enemies);

			_renderer.enabled = true;
		}

		public void OnExit()
		{
			var exitAreas = GetComponentsInChildren<ExitArea>();
			exitAreas.ToList().ForEach(area => area.SetEnable(false));

			KillAllEnemies(_enemies);

			_renderer.enabled = false;
		}

		private void CreateEnemies(List<CharacterController> enemies)
		{
			var enemySpawners = GetComponentsInChildren<EnemySpawner>().ToList();
			var enemyParent = transform;
			enemySpawners.ForEach(spawner => spawner.RequestInstantiate(enemyParent, enemies.Add));
		}

		private void KillAllEnemies(List<CharacterController> enemies)
		{
			enemies.ForEach(enemy => { if (enemy != null) enemy.Kill(); });
		}
	}
}