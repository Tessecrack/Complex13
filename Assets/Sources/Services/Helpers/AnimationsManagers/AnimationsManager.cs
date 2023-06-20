using UnityEngine;

public abstract class AnimationsManager
{
	protected Animator animator;
	protected CharacterAnimationState currentAnimationState;
	protected int[] idsAnimationsStrikes;
	public AnimationsManager(Animator animator)
	{
		this.animator = animator;
		currentAnimationState = new CharacterAnimationState();
	}

	public abstract void InitializeCloseCombatAnimations();

	public abstract void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState);
}
