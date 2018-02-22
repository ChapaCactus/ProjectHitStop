using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CCGames
{
	public class TalkEvent : IEvent
	{
		public void Call()
		{
			Debug.Log("会話イベントが呼ばれました.");
		}

		public void OnComplete()
		{
		}
	}
}