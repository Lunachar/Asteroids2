using UnityEngine;

namespace Player
{
    public class PlayerView: MonoBehaviour
    {
        //[SerializeField] private SpriteRenderer _spriteRenderer;

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        public void UpdateRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0f,0f,angle-90f);
        }
    }
}