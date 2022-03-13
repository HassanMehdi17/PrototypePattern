using System;
using System.Collections.Generic;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var internals = new Internals()
            {
                Lettuce = true,
                Tomatoes = true,
                Onions = true,
                Ketchup = true,
                Mustard = false,
                ExtraMeat = false
            };

            // initialization of the three sandwiches
            Sandwich sandwich1 = new( 10, internals);

            // example of what happens if you DON'T clone
            ISandwich sandwich2 = sandwich1;
            // example of what happens when you DO clone
            ISandwich sandwich3 = sandwich1.Clone();

            // display sandwich values before modification
            Console.WriteLine("Original values:");
            Console.WriteLine("sandwich #1:");
            DisplayValues(sandwich1);
            Console.WriteLine("sandwich #2:");
            DisplayValues(sandwich2);
            Console.WriteLine("sandwich #3:");
            DisplayValues(sandwich3);

            // change the internal values of sandwich 1
            Console.WriteLine("");
            sandwich1.Size = 11;
            sandwich1.Internals.ExtraMeat = true;
            sandwich1.Internals.Mustard = true;

            // display sandwich values after modifying sandwich 1.
            Console.WriteLine("Values of sandwiches after changing sandwich #1 size and internals:");
            Console.WriteLine("sandwich #1:");
            DisplayValues(sandwich1);
            Console.WriteLine("sandwich #2:");
            DisplayValues(sandwich2);
            Console.WriteLine("sandwich #3:");
            DisplayValues(sandwich3);

            ISandwich subSandwich = new SubSandwich(25, internals, "TESTBREAD");

            var sandwichList = new List<ISandwich>() { sandwich1, sandwich2, sandwich3, subSandwich};
            var clonedSandwichList = new List<ISandwich>();

            foreach (var sandwich in sandwichList)
                clonedSandwichList.Add(sandwich.Clone());

            Console.WriteLine("\nDisplay interfaced values for different types of sandwiches:");
            foreach (var sandwich in clonedSandwichList)
                DisplayValues(sandwich);
        }

        // this method just makes it easier to display the sandwich data
        public static void DisplayValues(ISandwich sandwich) =>
            Console.WriteLine($"size: {sandwich.Size}, Lettuce: {sandwich.Internals.Lettuce}, Tomatoes: {sandwich.Internals.Tomatoes}, " +
                $"Onions: {sandwich.Internals.Onions}, Ketchup: {sandwich.Internals.Ketchup}, Mustard: {sandwich.Internals.Mustard}, " +
                $"Extra Meat: {sandwich.Internals.ExtraMeat}");
    }

    public interface ISandwich
    {
        int Size { get; set; }
        Internals Internals { get; set; }
        ISandwich Clone();
    }

    // the sandwich model with a Clone method
    public class Sandwich : ISandwich
    {
        public int Size { get; set; }
        public Internals Internals { get; set; }

        public Sandwich(Sandwich sandwich)
        {
            Size = sandwich.Size;
            Internals = new Internals
            {
                Lettuce = sandwich.Internals.Lettuce,
                Tomatoes = sandwich.Internals.Tomatoes,
                Onions = sandwich.Internals.Onions,
                Ketchup = sandwich.Internals.Ketchup,
                Mustard = sandwich.Internals.Mustard,
                ExtraMeat = sandwich.Internals.ExtraMeat,
            };
        }
        public Sandwich(int size, Internals internals)
        {
            Size = size;
            Internals = internals;
        }

        // Clone method
        public virtual ISandwich Clone() => new Sandwich(this);
        
    }

    public class SubSandwich : Sandwich
    {
        public string BreadType { get; set; }

        public SubSandwich(SubSandwich sandwich)
            : base(sandwich.Size, sandwich.Internals)
        {
            BreadType = sandwich.BreadType;
        }

        public SubSandwich(int size, Internals internals, string breadType)
            : base(size, internals)
        {
            BreadType = breadType;
        }

        public override ISandwich Clone() => new SubSandwich(this);
    }

    public class Internals
    {
        public bool Lettuce { get; set; } = false;
        public bool Tomatoes { get; set; } = false;
        public bool Onions { get; set; } = false;
        public bool Ketchup { get; set; } = false;
        public bool Mustard { get; set; } = false;
        public bool ExtraMeat { get; set; } = false;
    }
}
