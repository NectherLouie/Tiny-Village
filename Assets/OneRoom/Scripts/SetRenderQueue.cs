﻿/*
	SetRenderQueue.cs
 
	Sets the RenderQueue of an object's materials on Awake. This will instance
	the materials, so the script won't interfere with other renderers that
	reference the same materials.
*/

using UnityEngine;

namespace OneRoom
{
	[AddComponentMenu("Rendering/SetRenderQueue")]
	public class SetRenderQueue : MonoBehaviour
	{

		[SerializeField]
		protected int[] m_queues = new int[] { 3000 };

		protected void Awake()
		{
			//Material[] materials = GetComponent<Renderer>().materials;
			//for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
			//{
			//	materials[i].renderQueue = m_queues[i];
			//}

			var renders = GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renders)
			{
				r.material.renderQueue = 2000; // set their renderQueue
			}
		}
	}
}