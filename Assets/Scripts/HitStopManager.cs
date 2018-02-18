using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CCGames
{
	public class HitStopManager : SingletonMonoBehaviour<HitStopManager>
	{
		private List<IHitStop> _hitStoppableObjects = new List<IHitStop>();

		public bool IsHitStopping { get; private set; } = false;

		public void Subscribe(IHitStop obj)
		{
			_hitStoppableObjects.Add(obj);
		}

		public void RemoveSubscribe(IHitStop obj)
		{
			_hitStoppableObjects.Remove(obj);
		}

		public void CallStartHitStop()
		{
			if (IsHitStopping) return;

			IsHitStopping = true;

			_hitStoppableObjects.ForEach(obj => obj.Stop());

			StartCoroutine(HitStopTimer(1));
		}

		public void CallEjectHitStop()
		{
			IsHitStopping = false;

			_hitStoppableObjects.ForEach(obj => obj.Eject());
		}

		private IEnumerator HitStopTimer(float duration)
		{
			var wait = new WaitForSeconds(duration);
			yield return wait;

			CallEjectHitStop();
		}
	}
}