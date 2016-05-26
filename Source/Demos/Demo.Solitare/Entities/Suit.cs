using System;
using System.Collections.Generic;

namespace Demo.Solitare.Entities
{
    public enum SuitColor
    {
        Red, Black
    }

    public struct Suit : IEquatable<Suit>
    {
        private Suit(string name, SuitColor color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; }
        public SuitColor Color { get; }

        public static Suit Clubs => new Suit("Clubs", SuitColor.Black);
        public static Suit Diamonds => new Suit("Diamonds", SuitColor.Red);
        public static Suit Hearts => new Suit("Hearts", SuitColor.Red);
        public static Suit Spades => new Suit("Spades", SuitColor.Black);

        public static IEnumerable<Suit> GetAll()
        {
            return new[] {Clubs, Diamonds, Hearts, Spades};
        } 

        public bool Equals(Suit other)
        {
            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Suit && Equals((Suit)obj);
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Suit a, Suit b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Suit a, Suit b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}