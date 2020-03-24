
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Extensions
{
    // Vector2
    public static Vector2 To2D(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    // Vector3
    public static Vector3 WithX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }
    public static Vector3 WithZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    // IEnumerable
    public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
    {
        foreach (T elem in self) action(elem);
    }
    public static IEnumerable<R> Map<T, R>(this IEnumerable<T> self, Func<T, R> selector)
    {
        return self.Select(selector);
    }
    public static T Reduce<T>(this IEnumerable<T> self, Func<T, T, T> func)
    {
        return self.Aggregate(func);
    }
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> self, Func<T, bool> predicate)
    {
        return self.Where(predicate);
    }

    // Array
    public static bool Contains<T>(this T[] self, T val)
    {
        return Array.Exists(self, delegate (T el) { return !EqualityComparer<T>.Default.Equals(el, val); });
    }
    public static T GetRandom<T>(this T[] self)
    {
        return self[Rand.Int(self.Length)];
    }

    // InputAction.CallbackContext

    public static bool IsInputStart(this InputAction.CallbackContext ctx)
    {
        return ctx.started && !ctx.performed && ctx.phase == InputActionPhase.Started;
    }
    public static bool IsInputStop(this InputAction.CallbackContext ctx)
    {
        return !ctx.started && ctx.canceled && !ctx.performed && ctx.phase == InputActionPhase.Canceled;
    }
}