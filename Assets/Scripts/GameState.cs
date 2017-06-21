using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveGameFree
{

	[Serializable]
	public class GameState
	{

		public string sceneIndex = "Mapa2";

		public override string ToString ()
		{
			return string.Format ( "[GameState] Scene Index: {0}", sceneIndex );
		}

	}
}
