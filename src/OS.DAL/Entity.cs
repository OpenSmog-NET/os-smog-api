using System;

namespace OS.DAL
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : struct, IEquatable<TId>, IComparable<TId>
    {
        public TId Id { get; set; }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || (ReferenceEquals(b, null)))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return $"{GetType().ToString()}::{Id}".GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != this.GetType()) return false;
            if (Id.Equals(default(TId)) || (other.Id.Equals(default(TId)))) return false;

            return Id.Equals(other.Id);
        }
    }
}