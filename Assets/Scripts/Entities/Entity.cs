using System;

namespace Entities
{
    public class Entity : IEquatable<Entity>
    {
        private readonly Guid _id = Guid.NewGuid();
        
        public bool Equals(Entity other)
        {
            if (other == null) return false;
            return _id == other._id;
        }
    }
}