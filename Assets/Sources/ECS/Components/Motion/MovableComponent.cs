using UnityEngine;

public struct MovableComponent
{
	public Transform transform;
	public float moveSpeed;
	public Vector3 relativeVector;
	public Vector3 velocity;

	public float coefSmooth;
}