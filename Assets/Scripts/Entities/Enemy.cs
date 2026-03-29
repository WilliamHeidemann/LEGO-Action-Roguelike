using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        public Transform Transform { get; }

        public Enemy(Transform transform)
        {
            Transform = transform;
        }
    }
}