using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private GameObject? _hero;
        private IGameFactory _gameFactory = null!;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (_gameFactory.Hero == null) 
                _gameFactory.OnHeroCreated += InitializeHero;
            else
                InitializeHero(_gameFactory.Hero);

        }

        private void Update()
        {
            LookAtHero(_hero);
        }

        private void LookAtHero(GameObject? hero)
        {
            if (hero == null) 
                return;

            transform.localScale = hero.transform.position.x < transform.position.x
                ? new Vector3(-1, 1, 1)
                : new Vector3(1, 1, 1);
        }

        private void InitializeHero(GameObject hero)
        {
            _hero = hero;
            _gameFactory.OnHeroCreated -= InitializeHero;
        }
    }
}
