using UnityEngine;

namespace Asteroids2
{
    public class ToggleGOSwitchComponents : MonoBehaviour
    {
        private GameObject _gameObject;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private Collider _Collider;
        private bool activeFlag;

        public void Switch(GameObject GameObject)
        {
            _gameObject = GameObject;
            activeFlag = GameObject.activeSelf;
            //_spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
            //_rigidbody2D = _gameObject.GetComponent<Rigidbody2D>();
            //_Collider = _gameObject.GetComponent<Collider>();
            
            _gameObject.SetActive(!activeFlag);

            //_spriteRenderer.enabled = !_spriteRenderer.enabled;
            //_Collider.enabled = !_Collider.enabled;
        }
    }
}