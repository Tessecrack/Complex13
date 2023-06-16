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
			.Inc<StateAttackComponent>()
			.End();

		var rotatableComponents = world.GetPool<RotatableComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var characterStates = world.GetPool<StateAttackComponent>();

		foreach(var entity in filter)
		{
			ref var characterComponent = ref characterComponents.Get(entity);
			if (!characterComponent.instance.activeSelf)
			{
				continue;
			}

			ref var rotatableComponent = ref rotatableComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);
			ref var characterState = ref characterStates.Get(entity);
			var isStateAttack = characterState.state == CharacterState.Aiming;
			var direction = targetComponent.target - characterComponent.characterTransform.position;

			if (isStateAttack)
			{
				direction.y = 0;
				characterComponent.characterTransform.forward = Vector3.Slerp(characterComponent.characterTransform.forward,
					direction, rotatableComponent.coefSmooth);
			}
		}
	}
}
