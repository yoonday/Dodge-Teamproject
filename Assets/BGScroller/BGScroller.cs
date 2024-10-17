using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
	public float speed;
	private float offset;
	private MeshRenderer render;
	void Start()
	{
		render = GetComponent<MeshRenderer>();
	}

	void Update()
	{
		offset += Time.deltaTime * speed;
		render.material.mainTextureOffset = new Vector2(0, offset);
	}
}
