using System;
namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialization of the three sandwiches
            Sandwich sandwich1 = new(10, new Internals()
            {
                Lettuce = true,
                Tomatoes = true,
                Onions = true,
                Ketchup = true,
                Mustard = false,
                ExtraMeat = false
            });
            // example of what happens if you DON'T clone
            Sandwich sandwich2 = sandwich1;
            // example of what happens when you DO clone
            Sandwich sandwich3 = sandwich1.Clone();

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
        }

        // this method just makes it easier to display the sandwich data
        public static void DisplayValues(Sandwich sandwich) =>
            Console.WriteLine($"size: {sandwich.Size}, Lettuce: {sandwich.Internals.Lettuce}, Tomatoes: {sandwich.Internals.Tomatoes}, " +
                $"Onions: {sandwich.Internals.Onions}, Ketchup: {sandwich.Internals.Ketchup}, Mustard: {sandwich.Internals.Mustard}, " +
                $"Extra Meat: {sandwich.Internals.ExtraMeat}");
    }

    // the sandwich model with a Clone method
    public class Sandwich
    {
        public int Size;
        public Internals Internals;

        public Sandwich(int size, Internals internals)
        {
            Size = size;
            Internals = internals;
        }

        // Clone method
        public virtual Sandwich Clone()
        {
            Sandwich clone = (Sandwich)MemberwiseClone();
            clone.Internals = new Internals
            {
                Lettuce = Internals.Lettuce,
                Tomatoes = Internals.Tomatoes,
                Onions = Internals.Onions,
                Ketchup = Internals.Ketchup,
                Mustard = Internals.Mustard,
                ExtraMeat = Internals.ExtraMeat,
            };

            return clone;
        }
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
