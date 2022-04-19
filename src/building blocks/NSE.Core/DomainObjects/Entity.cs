using NSE.Core.Message;
using System;
using System.Collections.Generic;

namespace NSE.Core
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _events;
        public IReadOnlyCollection<Event> Notificacoes => _events?.AsReadOnly();

        public void AddEvent(Event @event)
        {
            _events = _events ?? new List<Event>();
            _events.Add(@event); 
        }
        public void RemoveEvent(Event @event)
        {
            _events?.Remove(@event);
        }
        public void ClearEvents()
        {
            _events?.Clear();
        }

        #region Validações
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(null, a) && ReferenceEquals(null, b))
                return true;

            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            if (a is null && b is null)
                return false;

            if (a is null || b is null)
                return true;

            return !a.Equals(b);
        }


        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        #endregion
    }
}
