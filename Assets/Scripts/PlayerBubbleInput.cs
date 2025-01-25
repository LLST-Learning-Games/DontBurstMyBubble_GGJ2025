using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerBubbleInput : MonoBehaviour
    {
        public UnityEvent<float> ScaleChanged = new();
        
        [SerializeField] private KeyCode[] inflateKeys = { KeyCode.W, KeyCode.C };
        [SerializeField] private KeyCode[] deflateKeys = { KeyCode.S, KeyCode.Z };
        
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
            
            if (IsPressed(inflateKeys) || Input.GetKey("joystick button 2"))
            {
                _playerBubble.Scale(inflateSpeed * Time.deltaTime);//.transform.localScale += Vector3.one * (inflateSpeed * Time.deltaTime);
                ScaleChanged.Invoke(_playerBubble.transform.localScale.x);
            }
            
            if (IsPressed(deflateKeys) || Input.GetKey("joystick button 1"))
            {
                _playerBubble.Scale(-inflateSpeed * Time.deltaTime);//.transform.localScale -= Vector3.one * (inflateSpeed * Time.deltaTime);
                ScaleChanged.Invoke(_playerBubble.transform.localScale.x);
            } 

            bool IsPressed(KeyCode[] keyCodes)
            {
                foreach (var keyCode in keyCodes)
                {
                    if (Input.GetKey(keyCode))
                        return true;
                }
                
                return false;
            }
        }
    }
}