using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBubbleInput : MonoBehaviour
    {
        [SerializeField] private Bubble _playerBubble;
        [SerializeField] private BubbleSpawner _playerBubbleSpawner;
        [SerializeField] private float inflateSpeed = 2f;
        
        void Update()
        {
            //HandleBubblePopInput();
            //HandleBubbleSpawnerInput();
            HandleInflateDeflate();
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

        void HandleInflateDeflate()
        {
            if (!_playerBubbleSpawner)
            {
                return;
            }
            
            if (Input.GetKey(KeyCode.C) || Input.GetKey("joystick button 2"))
            {
                _playerBubble.Scale(inflateSpeed * Time.deltaTime);//.transform.localScale += Vector3.one * (inflateSpeed * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.Z) || Input.GetKey("joystick button 1"))
            {
                _playerBubble.Scale(-inflateSpeed * Time.deltaTime);//.transform.localScale -= Vector3.one * (inflateSpeed * Time.deltaTime);
            }
        }
    }
}