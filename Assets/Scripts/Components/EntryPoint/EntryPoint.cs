using Components.UI;
using UnityEngine;
using UserInput;

namespace Components.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private ProgressBarHandler _progressBarHandler;
        [SerializeField] private ThreeOptionsHandler _threeOptionsHandler;

        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerShooting _playerShooting;
        [SerializeField] private PlayerMovement _playerMovement;

        private void Awake()
        {
            EntityWorld world = new();
            _enemySpawner.Initialize(world);
            _enemyMovement.Initialize(world);

            ExperienceTracker experienceTracker = new(5);
            experienceTracker.OnLevelUp += () => PauseManager.PauseMode = PauseMode.Paused;
            _playerShooting.Initialize(world, experienceTracker);

            ButtonActions buttonActions = new();
            UserActions userActions = new(buttonActions, _gameConfig.LongPressTimeInSeconds);

            _inputReader.Initialize(buttonActions);
            _playerMovement.Initialize(buttonActions);
            _progressBarHandler.Initialize(buttonActions, userActions, _gameConfig.LongPressTimeInSeconds);
            _threeOptionsHandler.Initialize(userActions);

            experienceTracker.OnLevelUp += () => _threeOptionsHandler.Propose(
                new Choice(() => print(1), () => print(2), () => print(3))
            );

            PauseManager.PauseMode = PauseMode.Playing;
        }
    }
}