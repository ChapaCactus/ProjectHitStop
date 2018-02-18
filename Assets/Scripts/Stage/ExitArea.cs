using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CCGames.Constants;

namespace CCGames
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class ExitArea : MonoBehaviour
	{
		[SerializeField]
		private BoxCollider2D _exitArea;

		[SerializeField]
		private Directions _exitDirection = Directions.Up;

		public void SetEnable(bool enable) => _exitArea.enabled = enable;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!collision.CompareTag("Player")) return;

			FieldController.I.Move(_exitDirection);
		}
	}
}