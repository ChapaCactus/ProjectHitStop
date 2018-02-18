using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCGames
{
	public static class GameManager
	{
		public static int ComboCount { get; private set; } = 0;

		public static void AddCombo(int add = 1)
		{
			ComboCount += add;
			UIManager.I.HitCountText.UpdateCount(ComboCount);
		}
	}
}