using Leopotam.EcsLite;
using UnityEngine;
public class CharacterRotationSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<RotatableComponent>()
			.Inc<CharacterComponent>()
			.Inc<TargetComponent>()
			.Inc<CharacterStateAttackComponent>()
			.End();

		var rotatableComponents = world.GetPool<RotatableComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var characterStates = world.GetPool<CharacterStateAttackComponent>();

		foreach(var entity in filter)
		{
			ref var rotatableComponent = ref rotatableComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var characterState = ref characterStates.Get(entity);

			var isStateAttack = characterState.characterState == CharacterState.Aiming;

			var direction = isStateAttack ? targetComponent.target - characterComponent.characterTransform.position
				: characterComponent.characterTransform.forward;

			if (characterComponent.characterController.velocity.magnitude > 0 || isStateAttack)
			{
				direction.y = 0;
				characterComponent.characterTransform.forward = Vector3.Slerp(characterComponent.characterTransform.forward, direction, 0.3f);
			}
			else if (!isStateAttack)
			{
				characterComponent.characterTransform.forward = characterComponent.characterController.velocity;
			}
		}
	}
}
