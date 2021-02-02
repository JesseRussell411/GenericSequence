using NUnit.Framework;
using System.Linq;
using JesseRussell.Collections;

namespace SequenceTest
{
    public class dog
    {
        public dog(string name) => Name = name;
        public string Name { get; set; }
        public override string ToString() => Name;
        public override int GetHashCode() => Name.GetHashCode();
    }
    public class Tests
    {
        Sequence<dog> dogs;
        Sequence<dog> sameDogs;
        dog special;
        [SetUp]
        public void Setup()
        {
            special = new dog("DOG");
            dogs = new dog[] {
                new dog("fido"),
                new dog("gadget"),
                new dog("moxy"),
                new dog("clide"),
                special
            }.ToSequence();
            sameDogs = dogs;
        }

        [Test, Order(0)]
        public void EqualityByReference()
        {
            if (!(dogs == sameDogs)) Assert.Fail("==");
            if (dogs != sameDogs) Assert.Fail("!=");
            if (!dogs.Equals(sameDogs)) Assert.Fail("Equals()");
            Assert.AreEqual(dogs, sameDogs);
        }
        [Test, Order(0)]
        public void EqualityByValue()
        {
            sameDogs = dogs.ToSequence(); // sameDogs now contains a different immutableArray<dog>, so equality cannot be determined by reference

            //dogs == sameDogs:
            if (!(dogs == sameDogs)) Assert.Fail("==");
            if (dogs != sameDogs) Assert.Fail("!=");
            if (!dogs.Equals(sameDogs)) Assert.Fail("Equals()");
            Assert.AreEqual(dogs, sameDogs);

            sameDogs = sameDogs.SkipLast(1).ToSequence(); // sameDogs now contains a different sequence

            //dogs != sameDogs:
            if (dogs == sameDogs) Assert.Fail("==");
            if (!(dogs != sameDogs)) Assert.Fail("!=");
            if (dogs.Equals(sameDogs)) Assert.Fail("Equals()");
            Assert.AreNotEqual(dogs, sameDogs);

            sameDogs = sameDogs.Append(special).ToSequence(); // sameDogs now contains the same sequence again, but it is definitely not a reference.

            // dogs == sameDogs:
            if (!(dogs == sameDogs)) Assert.Fail("==");
            if (dogs != sameDogs) Assert.Fail("!=");
            if (!dogs.Equals(sameDogs)) Assert.Fail("Equals()");
            Assert.AreEqual(dogs, sameDogs);
        }
    }
}