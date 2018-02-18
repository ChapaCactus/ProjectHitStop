using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CCGames
{
	[RequireComponent(typeof(Text))]
	public class HitCountText : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		private void Awake()
		{
			_text.enabled = false;
		}

		public void UpdateCount(int count)
		{
			_text.text = $"{count} HIT!";

			_text.enabled = true;
		}
	}
}