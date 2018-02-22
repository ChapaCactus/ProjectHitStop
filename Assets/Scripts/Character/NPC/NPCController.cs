using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCGames
{
	public class NPCController : MonoBehaviour
	{
		public bool IsTalkable => _model.OnTalkEvent != null;

		private NPCModel _model { get; set; }

		public void Setup(NPCModel model)
		{
			_model = model;
		}

		public void StartTalking()
		{
			if (!IsTalkable) return;
		}
	}

	public class NPCModel
	{
		public IEvent OnTalkEvent { get; private set; }
		public IEvent OnDeadEvent { get; private set; }
	}
}