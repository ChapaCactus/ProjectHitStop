using System;

using UnityEngine.Assertions;

namespace CCGames
{
	public static class SafeCallback
	{
		public static void SafeCall<T>(this Action<T> action, T arg)
		{
			if (action == null) return;

			action(arg);
		}

		public static void SafeCall(this Action action)
		{
			if (action == null) return;

			action();
		}
	}
}