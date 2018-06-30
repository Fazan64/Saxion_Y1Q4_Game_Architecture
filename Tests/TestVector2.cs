using NUnit.Framework;
using System;
using Engine;
using static Engine.Mathf;

[TestFixture]
public class TestVector2
{
    [Test]
    public void Constructor()
    {
        var vector = new Vector2(5f, 8f);

        Assert.That(vector.x, Is.EqualTo(5f));
        Assert.That(vector.y, Is.EqualTo(8f));
    }

    [Test]
    public void EqualityOperator()
    {
        var a = new Vector2(1f, 1f);
        var b = new Vector2(1f, 1f);
        var c = new Vector2(1f, 2f);

        Assert.That(a, Is.EqualTo(b));
        Assert.That(a, Is.Not.EqualTo(c));

        Assert.True (a == b);
        Assert.False(a == c);
        Assert.True (a != c);
        Assert.False(a != b);
    }

    [Test]
    public void MathOperators()
    {
        var a = new Vector2(1f, 2f);
        var b = new Vector2(4f, 8f);
        float scale = 16f;

        Vector2 negated    = -a;
        Vector2 sum        = a + b;
        Vector2 difference = a - b;
        Vector2 multiplied = a * scale;
        Vector2 divided    = a / scale;

        Assert.That(negated   , Is.EqualTo(new Vector2(-1f, -2f)));
        Assert.That(sum       , Is.EqualTo(new Vector2( 5f, 10f)));
        Assert.That(difference, Is.EqualTo(new Vector2(-3f, -6f)));
        Assert.That(multiplied, Is.EqualTo(new Vector2(16f, 32f)));
        Assert.That(divided   , Is.EqualTo(new Vector2(1f / 16f, 2f / 16f)));
    }

    [Test]
    public void Add()
    {
        var a = new Vector2(10f, 10f);
        var b = new Vector2(5f , -5f);

        Vector2 result = a.Add(b);

        Assert.That(result, Is.EqualTo(a));
        Assert.That(result, Is.EqualTo(new Vector2(10f + 5f, 10f + (-5f))));
    }

    [Test]
    public void Subtract()
    {
        var a = new Vector2(10f, 10f);
        var b = new Vector2(5f, -5f);

        var result = a.Subtract(b);

        Assert.That(a, Is.EqualTo(result));
        Assert.AreEqual(result, new Vector2(10f - 5f, 10f - (-5f)));
    }

    [Test]
    public void Multiply()
    {
        var vector = new Vector2(10f, 10f);
        var factor = 5f;

        Vector2 result = vector * factor;

        Assert.That(result, Is.Not.EqualTo(vector));
        Assert.AreEqual(result, new Vector2(10f * 5f, 10f * 5f));
    }

    [Test]
    public void MagnitudeNonzero()
    {
        var vector = new Vector2(10f, 10f);

        var length = vector.magnitude;

        Assert.AreEqual(length, Sqrt(10f * 10f + 10f * 10f));
    }

    [Test]
    public void MagnitudeZero()
    {
        var vector = new Vector2(0f, 0f);

        float length = vector.magnitude;

        Assert.AreEqual(length, 0f);
    }

    [Test]
    public void Normalize()
    {
        var vector = new Vector2(10f, 5f);
        float length = vector.magnitude;

        Vector2 result = vector.Normalize();

        Assert.That(vector, Is.EqualTo(result));
        Assert.That(result.magnitude, Is.EqualTo(1f));
        Assert.That(result, Is.EqualTo(new Vector2(10f / length, 5f / length)));
    }

    [Test]
    public void TestToString()
    {
        var vector = new Vector2(10f, 5f);

        string asString = vector.ToString();

        Assert.AreEqual(asString, "[Vector2 10, 5]");
    }

    [Test, TestCaseSource("angleTestCases")]
    public void GetAngle(Vector2 vector, float angleRadians, float angleDegrees)
    {
        Assert.AreEqual(vector.GetAngleRadians(), angleRadians);
        Assert.AreEqual(vector.GetAngleDegrees(), angleDegrees);
    }

    static TestCaseData[] angleTestCases = 
    {
        new TestCaseData(new Vector2(10f, 10f), PI / 4f, 45f),
        new TestCaseData(new Vector2(-1f,  0f), PI     , 180f)
    };

    [Test]
    public void RotateRadians()
    {
        var vec = new Vector2(1f, 0f);
        float length = vec.magnitude;

        float rotationAngle = PI / 2;
        vec.RotateRadians(rotationAngle);

        Assert.AreEqual(rotationAngle, vec.GetAngleRadians());
        Assert.AreEqual(length, vec.magnitude);
    }

    [Test]
    public void RotateAroundRadians()
    {
        var centerOfRotation = new Vector2(4f, 3f);

        float rotationAngle = PI / 2f;
        var rotated = new Vector2(2f, 0f)
            .Add(centerOfRotation)
            .RotateAroundRadians(rotationAngle, centerOfRotation);

        Assert.AreEqual(new Vector2(0f, 2f) + centerOfRotation, rotated);
    }

    [Test]
    public void SetAngleRadians()
    {
        var vec = new Vector2(2f, 1f);
        float length = vec.magnitude;

        float newAngle = PI / 2;
        vec.SetAngleRadians(newAngle);

        Assert.AreEqual(vec.GetAngleRadians(), newAngle);
        Assert.AreEqual(length, vec.magnitude);
    }

    [Test]
    public void IsZero()
    {
        var a = new Vector2( 0f, 0f);
        var b = new Vector2( 1f, 1f) / 100000f;
        var c = new Vector2(-1f, 1f) / 100000f;

        var d = new Vector2(10f ,  0f  );
        var e = new Vector2(0f  ,  10f );
        var f = new Vector2(-10f, -0.1f);

        Assert.IsTrue(a.isZero);
        Assert.IsTrue(b.isZero);
        Assert.IsTrue(c.isZero);

        Assert.IsFalse(d.isZero);
        Assert.IsFalse(e.isZero);
        Assert.IsFalse(f.isZero);
    }

    [Test, TestCaseSource("reflectTestCases")]
    public void Reflected(Vector2 vector, Vector2 normal, Vector2 expectedResult)
    {
        Assert.That(vector.Reflected(normal), Is.EqualTo(expectedResult));
    }

    public static TestCaseData[] reflectTestCases = 
    {
        new TestCaseData(new Vector2(10f, -10f), Vector2.up,    new Vector2( 10f,  10f)),
        new TestCaseData(new Vector2(10f, -10f), Vector2.right, new Vector2(-10f, -10f)),
        new TestCaseData(new Vector2(10f, -10f), Vector2.one,   new Vector2( 10f, -10f)),
        new TestCaseData(new Vector2(-1f,   0f), Vector2.one,   new Vector2(  0f,   1f)),
        new TestCaseData(new Vector2(-1f,   0f), Vector2.one.normalized, new Vector2(0f, 1f))
    };
}
