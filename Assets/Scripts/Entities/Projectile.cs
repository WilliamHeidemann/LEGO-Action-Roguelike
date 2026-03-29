using UnityEngine;

namespace Entities
{
    public class Projectile : Entity
    {
        public Projectile(Transform transform)
        {
            Transform = transform;
        }

        public Transform Transform { get; }
    }
}