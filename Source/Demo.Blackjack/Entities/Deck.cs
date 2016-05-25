using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Solitare.Entities
{
    public class Deck<T> : List<T>
    {
        public Deck()
        {
        }

        public T Draw()
        {
            if (Count == 0)
                throw new InvalidOperationException("Cannot draw from an empty deck");

            var item = this[0];
            RemoveAt(0);
            return item; 
        }

        public void Shuffle(Random random)
        {
            for (var i = 0; i < Count; i++)
            {
                var temp = this[i];
                var index = random.Next(0, Count);
                this[i] = this[index];
                this[index] = temp;
            }
        }
    }
}