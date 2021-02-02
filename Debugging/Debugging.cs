using System.Collections.Immutable;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using JesseRussell.Collections;

namespace Debugging
{
    public class dog
    {
        public dog(string name) => Name = name;
        public string Name { get; set; }
        public override string ToString() => Name;
        public override int GetHashCode() => Name.GetHashCode();
    }
    class Debugging
    {
        static void Main(string[] args)
        {
            dog special = new dog("noName");
            Sequence<dog> dogs = new dog[] { 
                new dog("fido"),
                new dog("gadget"),
                new dog("moxy"),
                new dog("clide"),
                special
            }.ToSequence();
            Sequence<dog> sameDogs = new Sequence<dog>(dogs.Elements);
            
            Console.WriteLine(dogs.Elements == sameDogs.Elements);
            sameDogs = sameDogs.Elements.ToSequence();
            Console.WriteLine(dogs.Elements == sameDogs.Elements);
            Console.WriteLine(sameDogs.ToString());
            sameDogs = ((IEnumerable<dog>)null).ToSequence();
            Console.WriteLine(sameDogs.ToString());
            
        }
    }
}
