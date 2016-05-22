using System;
using System.Collections.Generic;

namespace Demo.Solitare.Entities
{
    public struct Rank : IComparable<Rank>, IEquatable<Rank>
    {
        private Rank(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public int Value { get; }

        public static Rank Ace => new Rank("A", 1);
        public static Rank Two => new Rank("2", 2);
        public static Rank Three => new Rank("3", 3);
        public static Rank Four => new Rank("4", 4);
        public static Rank Five => new Rank("5", 5);
        public static Rank Six => new Rank("6", 6);
        public static Rank Seven => new Rank("7", 7);
        public static Rank Eight => new Rank("8", 8);
        public static Rank Nine => new Rank("9", 9);
        public static Rank Ten => new Rank("10", 10);
        public static Rank Jack => new Rank("J", 11);
        public static Rank Queen => new Rank("Q", 12);
        public static Rank King => new Rank("K", 13);

        public static IEnumerable<Rank> GetAll()
        {
            return new[] { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
        }

        public int CompareTo(Rank other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Rank other)
        {
            return Value == other.Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}