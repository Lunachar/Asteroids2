using UnityEngine;

namespace Player
{
    public class PlayerView: MonoBehaviour
    {
        //[SerializeField] private SpriteRenderer _spriteRenderer;

        // Update the position of the player in the view
        public void UpdatePosition(Vector2 position)
        {
            transform.position = position; // Set the transform position to the given position
        }

        // Update the rotation of the player in the view
        public void UpdateRotation(float angle)
        {
            // Set the transform rotation using Euler angles, adjusting for the offset
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }
}