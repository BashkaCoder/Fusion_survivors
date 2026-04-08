using System.Linq;
using Gameplay.CharacterBehaviour;
using Gameplay.Stats;
using Infrastructure.Input;
using Infrastructure.Pools;
using Infrastructure.Spawners;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        private CharacterMovement _enemyMovement;
        private CharacterMeleeAttack _enemyAttack; 
        private CharacterHealth _enemyHealth;
        private CharacterStatsRuntime _enemyStats;
        
        private NearestPlayerDirectionProvider _nearestPlayerDirectionProvider;
        private ExperiencePickupSpawner _experiencePickupSpawner;

        private EnemyPool _pool;
        
        [Inject]
        private void Construct(
            CharacterMovement characterMovement, 
            CharacterMeleeAttack characterAttack, 
            CharacterHealth characterHealth,
            CharacterStatsRuntime enemyStats,
            NearestPlayerDirectionProvider nearestPlayerDirectionProvider,
            ExperiencePickupSpawner experiencePickupSpawner,
            EnemyPool pool)
        {
            _enemyMovement = characterMovement;
            _enemyAttack = characterAttack;
            _enemyHealth = characterHealth;
            _enemyStats = enemyStats;
            _nearestPlayerDirectionProvider = nearestPlayerDirectionProvider;
            _experiencePickupSpawner = experiencePickupSpawner;
            _pool = pool;
        }

        private void OnEnable()
        {
            _enemyHealth.OnDied += HandleEnemyDeath;

            _enemyAttack.SetAttackDamage(_enemyStats.Get(StatId.AttackDamage));
            _enemyAttack.SetAttackSpeed(_enemyStats.Get(StatId.AttackSpeed));
            _enemyHealth.SetMaxHealth(_enemyStats.Get(StatId.MaxHealth));
            _enemyHealth.RestoreToMax();
            _enemyMovement.SetMoveSpeed(_enemyStats.Get(StatId.MoveSpeed));
            
            _nearestPlayerDirectionProvider.SetEnemyTransform(transform);
        }

        private void OnDisable()
        {
            _enemyHealth.OnDied -= HandleEnemyDeath;
        }

        private void HandleEnemyDeath()
        {
            if (_pool.InactiveItems.Contains(this)) return;
            
            _experiencePickupSpawner.Spawn(transform.position);
            _pool.Despawn(this);
        }
    }
}
