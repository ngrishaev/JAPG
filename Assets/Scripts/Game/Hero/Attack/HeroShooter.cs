using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.StaticData;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero.Attack
{
    public class HeroShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint = null!;
        [Header("Prefabs")]
        [SerializeField] private HeroBullet _bulletPrefab = null!;
        [SerializeField] private HeroRocket _rocketPrefab = null!;
        [SerializeField] private HeroStun _stunPrefab = null!;
        
        private readonly List<CooldownChecker> _cooldowns = new();
        
        private PistolShooter _pistolShooter = null!;
        private RocketShooter _rocketShooter = null!;
        private StunShooter _stunShooter = null!;
        
        private void Start()
        {
            var pistolStaticData = AllServices.Container.Single<IStaticDataService>().GetPlayerStaticData().PistolStaticData;
            var rocketStaticData = AllServices.Container.Single<IStaticDataService>().GetPlayerStaticData().RocketStaticData;
            var stunStaticData = AllServices.Container.Single<IStaticDataService>().GetPlayerStaticData().StunStaticData;
            
            _pistolShooter = new PistolShooter(
                GetCooldownChecker(pistolStaticData.Cooldown),
                _shootPoint,
                _bulletPrefab,
                pistolStaticData);
            
            _rocketShooter = new RocketShooter(
                GetCooldownChecker(rocketStaticData.Cooldown),
                _shootPoint,
                _rocketPrefab,
                rocketStaticData);
            
            _stunShooter = new StunShooter(
                GetCooldownChecker(stunStaticData.Cooldown),
                _shootPoint,
                _stunPrefab,
                stunStaticData);
        }

        private void Update()
        {
            foreach (var cooldown in _cooldowns) 
                cooldown.Update(Time.deltaTime);
        }

        public void TryShootBullet()
        {
            _pistolShooter.TryShoot(GetDirection());
        }
        
        public void TryShootStun()
        {
            _stunShooter.TryShoot(GetDirection());
        }
        
        public void TryShootRocket()
        {
            _rocketShooter.TryShoot(GetDirection());
        }

        private Direction GetDirection() => 
            transform.lossyScale.x > 0 ? Direction.Right : Direction.Left;
        
        private CooldownChecker GetCooldownChecker(float cooldown)
        {
            var cooldownChecker = new CooldownChecker(cooldown);
            _cooldowns.Add(cooldownChecker);
            return cooldownChecker;
        }
    }
}