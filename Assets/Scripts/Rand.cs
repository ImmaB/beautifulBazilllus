using System;

public static class Rand
{
    private static System.Random generator = new System.Random();

    public static bool Bool(float prob) { return generator.NextDouble() < prob; }

    public static int Int() { return generator.Next(); }
    public static int Int(int max) { return generator.Next(max); }
    public static int Int(int min, int max) { return generator.Next(min, max); }

    public static float Float() { return (float)generator.NextDouble(); }
    public static float Float(float max) { return (float)generator.NextDouble() * max; }
    public static float Float(float min, float max) { return (float)generator.NextDouble() * (max - min) + min; }
}