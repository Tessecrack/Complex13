using Leopotam.EcsLite;

public class CharacterAnimationSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AnimatorComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponTypeComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var animators = world.GetPool<AnimatorComponent>();
		var movables = world.GetPool<MovableComponent>();
		var weaponTypes = world.GetPool<WeaponTypeComponent>();
		var attacks = world.GetPool<AttackComponent>();
		var attackStates = world.GetPool<StateAttackComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(int entity in filter)
		{
			ref var enablerComponent = ref enablers.Get(entity);
			if (!enablerComponent.isEnabled)
			{
				continue;
			}
			ref var animatorComponent = ref animators.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var weaponType = ref weaponTypes.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var attackState = ref attackStates.Get(entity);

			var currentState = attackState.state;

			animatorComponent.animationState.IsMoving = movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0;
			animatorComponent.animationState.CurrentTypeWeapon = weaponType.typeWeapon;
			animatorComponent.animationState.VerticalMoveValue = movableComponent.relativeVector.z;
			animatorComponent.animationState.HorizontalMoveValue = movableComponent.relativeVector.x;
			animatorComponent.animationState.IsAimingState = currentState == CharacterState.Aiming;
			animatorComponent.animationState.IsMeleeAttack = attackState.isMeleeAttack;

			animatorComponent.animationsManager.ChangeAnimationsState(animatorComponent.animationState);
		}
	}
}
