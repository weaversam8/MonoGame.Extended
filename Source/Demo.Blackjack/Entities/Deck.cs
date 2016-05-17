using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Solitare.Entities
{
    public class Deck<T> : Stack<T>
    {
        public Deck()
        {
        }

        public T Draw()
        {
            if (Count == 0)
                throw new InvalidOperationException("Cannot draw from an empty deck");
           
            return Pop(); 
        }
    }
}