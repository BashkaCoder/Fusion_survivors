using Gameplay;
using Infrastructure.Pools;
using ScriptableObjects;
using UnityEngine;
using Zenject;


namespace Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "GameplayConfigsInstaller", menuName = "Installers/GameplayConfigsInstaller")]
    public class GameplayConfigsInstaller : ScriptableObjectInstaller
    {
        [Header("Global binds")]
        [SerializeField] private WorldCombatConfig _worldCombatConfig;
        
        [Header("Player related binds")]
        [SerializeField] private PlayerLevelConfig _playerLevelConfig;
        [SerializeField] private UpgradeValuesConfig _upgradeValuesConfig;
        [SerializeField] private CharacterStatsConfig _playerStatsConfig;
        
        [Header("Enemy related binds")]
        [SerializeField] private CharacterStatsConfig _enemyStatsConfig;
        
        [Header("Prefabs for pools")]
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private ExperiencePickup _experiencePickupPrefab;
        
        public override void InstallBindings()
        {
            BindGlobalInstances();
            BindPlayerInstances();
            BindEnemyInstances();
            
            BindPools();
        }

        private void BindGlobalInstances()
        {
            Container.BindInstance(_worldCombatConfig).AsSingle();
        }

        private void BindPlayerInstances()
        {
            Container.BindInstance(_upgradeValuesConfig).AsSingle();
            Container.BindInstance(_playerLevelConfig).AsSingle();
            Container.BindInstance(_playerStatsConfig).WithId(CharacterId.Player).AsCached();
        }

        private void BindEnemyInstances()
        {
            Container.BindInstance(_enemyStatsConfig).WithId(CharacterId.Enemy).AsCached();
        }

        private void BindPools()
        {
            Container.BindMemoryPool<PlayerController, PlayerPool>()
                .WithInitialSize(4)
                .FromComponentInNewPrefab(_playerPrefab)
                .UnderTransformGroup("Players")
                .AsCached();
            
            Container.BindMemoryPool<EnemyController, EnemyPool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemies")
                .AsCached();

            Container.BindMemoryPool<Bullet, BulletPool>()
                .WithInitialSize(32)
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransformGroup("Projectiles")
                .AsCached();


            Container.BindMemoryPool<ExperiencePickup, ExperiencePickupPool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefab(_experiencePickupPrefab)
                .UnderTransformGroup("ExperiencePickups")
                .AsCached();
        }
    }
}