using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBubbleInput : MonoBehaviour
    {
        [SerializeField] private Bubble _playerBubble;
        [SerializeField] private BubbleSpawner _playerBubbleSpawner;
        
        void Update()
        {
            HandleBubblePopInput();
            HandleBubbleSpawnerInput();
        }

        private void HandleBubblePopInput()
        {
            if (!_playerBubble)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _playerBubble.PopBondedBubbles();
            }
        }

        private void HandleBubbleSpawnerInput()
        {
            if (!_playerBubbleSpawner)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerBubbleSpawner.SetSpawningActive(true);
            }
            
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _playerBubbleSpawner.SetSpawningActive(false);
            }
        }
    }
}