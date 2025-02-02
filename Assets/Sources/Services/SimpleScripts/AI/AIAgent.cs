using UnityEngine;

public class AIAgent
{
	private Transform transform;
	private bool hasRangeAttack;
	private bool hasMeleeAttack;

	private float rangeDetection = 5.0f;
	private float distanceMeleeAttack = 1.5f;
	private float distanceRangeAttack = 5.0f;

	private bool isTargetDetected;
	private bool canAttack;

	private bool isEnabled = true;

	public void SetRangeAttack(bool hasRangeAttack)
	{
		this.hasRangeAttack = hasRangeAttack;
	}

	public void SetMeleeAttack(bool hasMeleeAttack)
	{
		this.hasMeleeAttack = hasMeleeAttack;
	}

	public void SetTransform(in Transform transform)
	{
		this.transform = transform;
	}

	public void SetRangeDetection(float rangeDetection)
	{
		this.rangeDetection = rangeDetection;
	}

	public void SetDistanceMeleeAttack(float rangeMeleeAttack)
	{
		this.distanceMeleeAttack = rangeMeleeAttack;
	}

	public void SetDistanceRangeAttack(float distanceRangeAttack)
	{
		this.distanceRangeAttack = distanceRangeAttack;
	}

	public bool IsDetectTarget(in Vector3 target)
	{
		if (isEnabled == false)
		{
			return false;
		}

		var distance = target - transform.position;
		return isTargetDetected = rangeDetection >= distance.magnitude;
	}

	public bool CanMeleeAttack(in Vector3 target)
	{
		if (isEnabled == false)
		{
			return false;
		}

		if (hasMeleeAttack)
		{
			isTargetDetected = true;
			var distance = target - transform.position;
			return canAttack = distanceMeleeAttack >= distance.magnitude;
		}
		return false;
	}

	public bool CanRangeAttack(in Vector3 target)
	{
		if (isEnabled == false)
		{
			return false;
		}

		if (hasRangeAttack)
		{
			isTargetDetected = true;
			var distance = target - transform.position;
			return canAttack = distanceRangeAttack >= distance.magnitude;
		}
		return false;
	}

	public Vector3 MoveToTarget(in Vector3 target)
	{
		if (isEnabled == false)
		{
			return Vector3.zero;
		}

		if (isTargetDetected)
		{
			if (hasRangeAttack && CanRangeAttack(target))
			{
				return Vector3.zero;
			}
			else if (hasMeleeAttack && CanMeleeAttack(target))
			{
				return Vector3.zero;
			}
		}
		return (target - Position).normalized;
	}

	public float DistanceAttack => distanceMeleeAttack;

	public Vector3 Position => transform.position;

	public void Disable()
	{
		isEnabled = false;
	}
}
