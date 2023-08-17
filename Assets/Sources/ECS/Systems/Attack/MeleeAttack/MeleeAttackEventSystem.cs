using Leopotam.EcsLite;

public class MeleeAttackEventSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<MeleeAttackComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var attackEvents = world.GetPool<MeleeAttackComponent>(); 
		var attackComponents = world.GetPool<AttackComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var stateAttackComponents = world.GetPool<StateAttackComponent>();

		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var attackComponent = ref attackComponents.Get(entity);
			if (attackComponent.typeAttack == TypeAttack.Range)
			{
				continue;
			}
			ref var stateAttack = ref stateAttackComponents.Get(entity);
			ref var attackEvent = ref attackEvents.Get(entity);

			if (attackEvent.meleeAttack.IsAttackInProcess)
			{
				continue;
			}
			else
			{
				stateAttack.isMeleeAttack = false;
			}

			if (stateAttack.state == CharacterState.Idle)
			{
				continue;
			}

			if (attackComponent.isStartAttack)
			{
				stateAttack.isMeleeAttack = true;
			}
		}
	}
}