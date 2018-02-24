using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CCGames
{
	public class CharacterModel
	{
		public EnemyID Identifier;

		public string TargetTag = "";
		public string PrefabName = "";

		public int Health = 0;
		public int MaxHealth = 0;

		public bool IsDead => Health <= 0;

		public bool IsPlayable = false;
		public bool IsHitStopping = false;
		public bool IsAttacking = false;

		public void ResetHealth() => Health = MaxHealth;

		public string PrefabPath => $"Prefabs/Character/{PrefabName}";

		public static CharacterModel CreateEnemyData(EnemyID id)
		{
			var model = new CharacterModel();

			model.Identifier = id;
			model.TargetTag = "Player";
			model.PrefabName = "Character";

			model.MaxHealth = 1;
			model.ResetHealth();

			model.IsPlayable = false;

			return model;
		}

		public static CharacterModel CreatePlayerData()
		{
			var model = new CharacterModel();

			model.TargetTag = "Enemy";
			model.PrefabName = "Player";

			model.MaxHealth = 1;
			model.ResetHealth();

			model.IsPlayable = true;

			return model;
		}

		public void Damage(int damage, Action onDead)
		{
			if (IsDead) return;

			Health -= damage;
			if (Health < 0) Health = 0;

			if(IsDead)
			{
				onDead.SafeCall();
			}
		}

		public void Cure(int point)
		{
			Health += point;
			if (Health >= MaxHealth)
				ResetHealth();
		}
	}
}