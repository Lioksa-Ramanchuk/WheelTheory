namespace WheelTheory;

using System;

class Program
{
    static void PrintTable(string operation, Func<WheelNumber, WheelNumber, WheelNumber> op)
    {
        WheelNumber[] values = [WheelNumber.Nullity, -WheelNumber.Infinity, -5, -1, -0.0, 0, 1, 5, WheelNumber.Infinity, WheelNumber.Nullity];
        Console.WriteLine($"{operation} Table:");
        Console.Write("\t");
        foreach (var x in values) Console.Write($"{x}\t");
        Console.WriteLine();
        foreach (var y in values)
        {
            Console.Write($"{y}\t");
            foreach (var x in values)
            {
                Console.Write($"{op(x, y)}\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void PrintReciprocalTable()
    {
        WheelNumber[] values = { WheelNumber.Nullity, -WheelNumber.Infinity, -5, -1, -0.0, 0, 1, 5, WheelNumber.Infinity, WheelNumber.Nullity };
        Console.WriteLine("Reciprocal Table:");
        foreach (var x in values) Console.WriteLine($"{x}\t-> {x.Reciprocal()}");
        Console.WriteLine();
    }

    static void CheckLaws()
    {
        WheelNumber[] values = [0, 5, WheelNumber.Infinity, WheelNumber.Nullity];
        foreach (var x in values)
            foreach (var y in values)
                foreach (var z in values)
                {
                    Console.WriteLine($"x = {x}, y = {y}, z = {z}");
                    Console.WriteLine($"{x} + {y} = {y} + {x}:\t\t\t{(x + y) == (y + x)}");
                    Console.WriteLine($"{x} * {y} = {y} * {x}:\t\t\t{(x * y) == (y * x)}");
                    Console.WriteLine($"{x} + 0 = {x}:\t\t\t{(x + 0) == x}");
                    Console.WriteLine($"{x} * 1 = {x}:\t\t\t{(x * 1) == x}");
                    Console.WriteLine($"Rec(Rec({x})) = {x}:\t\t{x.Reciprocal().Reciprocal() == x}");
                    Console.WriteLine($"Rec({x} * {y}) = Rec({x}) * Rec({y}):\t{x.Reciprocal() * y.Reciprocal() == (x * y).Reciprocal()}");
                    Console.WriteLine($"({x}+{y})*{z}+0*{z} = {x}*{z}+{y}*{z}:\t\t{((x + y) * z + 0 * z) == (x * z + y * z)}");
                    Console.WriteLine($"({x}+{y}*{z})/{y} = {x}/{y}+{z}+0*{y}:\t\t{((x + y * z) / y) == (x / y + z + 0 * y)}");
                    Console.WriteLine($"({x}+0*{y})*{z} = {x}*{z}+0*{y}:\t\t{(x + 0 * y) * z == x * z + 0 * y}");
                    Console.WriteLine($"Rec({x}+0*{y}) = Rec({x})+0*{y}:\t{x.Reciprocal() + 0 * y == (x + 0 * y).Reciprocal()}");
                    Console.WriteLine($"0/0+{x}=0/0:\t\t\t{((WheelNumber)0 / (WheelNumber)0) + x == ((WheelNumber)0 / (WheelNumber)0)}");
                    Console.WriteLine($"0*{x}+0*{y}=0*{x}*{y}:\t\t\t{(0 * x + 0 * y) == (0 * x * y)}");
                    Console.WriteLine($"{x}/{x}=1+0*{x}/{x}:\t\t\t{x / x == 1 + 0 * x / x}");
                    Console.WriteLine($"{x}-{x}=0*{x}*{x}:\t\t\t{x - x == 0 * x * x}");
                    Console.WriteLine();
                }
    }

    static void Main()
    {
        PrintTable("Addition", (x, y) => x + y);
        PrintTable("Subtraction", (x, y) => x - y);
        PrintTable("Multiplication", (x, y) => x * y);
        PrintTable("Division", (x, y) => x / y);
        PrintReciprocalTable();
        CheckLaws();
    }
}
