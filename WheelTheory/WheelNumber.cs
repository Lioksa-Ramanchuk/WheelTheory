namespace WheelTheory;

using System;

public class WheelNumber
{
    private readonly double? value;
    public static readonly WheelNumber Infinity = new(double.PositiveInfinity);
    public static readonly WheelNumber Nullity = new(null);

    public WheelNumber(double? value)
    {
        if (value is not null)
        {
            if (double.IsNaN(value.Value))
            {
                this.value = null;
                return;
            }

            if (double.IsInfinity(value.Value))
            {
                this.value = double.PositiveInfinity;
                return;
            }

            if (value.Value == -0.0 || value.Value == 0.0)
            {
                this.value = 0.0;
                return;
            }
        }

        this.value = value;
    }

    public static implicit operator WheelNumber(double value) => new(value);
    public override string ToString() => value is null ? "⊥" : double.IsInfinity(value.Value) ? "∞" : value.ToString()!;

    public static WheelNumber operator +(WheelNumber a, WheelNumber b)
    {
        if (a.value is null || b.value is null) return Nullity;
        if (double.IsInfinity(a.value.Value) && double.IsInfinity(b.value.Value)) return Nullity;
        if (double.IsInfinity(a.value.Value) || double.IsInfinity(b.value.Value)) return Infinity;
        return new WheelNumber(a.value.Value + b.value.Value);
    }

    public static WheelNumber operator -(WheelNumber a)
    {
        if (a.value is null) return Nullity;
        if (double.IsInfinity(a.value.Value)) return Infinity;
        return new WheelNumber(-a.value.Value);
    }

    public static WheelNumber operator -(WheelNumber a, WheelNumber b)
    {
        if (a.value is null || b.value is null) return Nullity;
        if (double.IsInfinity(a.value.Value) && double.IsInfinity(b.value.Value)) return Nullity;
        if (double.IsInfinity(a.value.Value) || double.IsInfinity(b.value.Value)) return Infinity;
        return new WheelNumber(a.value.Value - b.value.Value);
    }

    public static WheelNumber operator *(WheelNumber a, WheelNumber b)
    {
        if (a.value is null || b.value is null) return Nullity;
        if (double.IsInfinity(a.value.Value) || double.IsInfinity(b.value.Value))
        {
            if (a.value == 0 || b.value == 0) return Nullity;
            return Infinity;
        }
        return new WheelNumber(a.value.Value * b.value.Value);
    }

    public static WheelNumber operator /(WheelNumber a, WheelNumber b) => a * b.Reciprocal();

    public WheelNumber Reciprocal()
    {
        if (value is null) return Nullity;
        if (value.Value == 0) return Infinity;
        if (double.IsInfinity(value.Value)) return new WheelNumber(0);
        return new WheelNumber(1 / value.Value);
    }

    public static bool operator ==(WheelNumber? a, WheelNumber? b)
    {
        if (a is null || b is null) return false;
        if (a.value is null && b.value is null) return true;
        if (a.value is null || b.value is null) return false;
        if (double.IsInfinity(a.value.Value) && double.IsInfinity(b.value.Value)) return true;

        const double tolerance = 1e-10;
        if (Math.Abs(a.value.Value - b.value.Value) < tolerance) return true;

        return false;
    }

    public static bool operator !=(WheelNumber a, WheelNumber b) => !(a == b);

    public override bool Equals(object? obj) => obj is WheelNumber other && this == other;
    public override int GetHashCode() => value?.GetHashCode() ?? 0;
}
