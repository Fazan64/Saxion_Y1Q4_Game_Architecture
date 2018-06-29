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

        Assert.AreEqual(vector.x, 5f);
        Assert.AreEqual(vector.y, 8f);
    }

    [Test]
    public void EqualityTrue()
    {
        var a = new Vector2(5f, 10f);
        var b = new Vector2(5f, 10f);

        Assert.That(a, Is.EqualTo(b));
    }

    [Test]
    public void EqualityFalse()
    {
        var a = new Vector2(5f, 10f);
        var b = new Vector2(5f, 11f);

        Assert.That(a, Is.Not.EqualTo(b));
    }

    [Test]
    public void EqualityOperator()
    {
        var a = new Vector2(10f, 10f);
        var b = new Vector2(10f, 10f);
        var c = new Vector2(15f, 13f);

        Assert.True (a == b);
        Assert.False(a == c);
        Assert.True (a != c);
        Assert.False(a != b);
    }

    [Test]
    public void Add()
    {
        var a = new Vector2(10f, 10f);
        var b = new Vector2(5f, -5f);

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

        Assert.AreEqual(length, Mathf.Sqrt(10f * 10f + 10f * 10f));
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

    /*[Test]
    public void GetUnitVector(
        [Range(0f, 2f * PI, step: 0.5f)] float angleRadians
    )
    {
        float angleDegrees = angleRadians * radToDeg;

        var vecFromRadians = Vector2.GetUnitVectorRadians(angleRadians);
        var vecFromDegrees = Vector2.GetUnitVectorDegrees(angleDegrees);

        Assert.AreEqual(vecFromRadians, vecFromDegrees);
        Assert.AreEqual(1f, vecFromRadians.Length(), delta: 0.001f);
    }*/

    [Test, TestCaseSource("reflectTestCases")]
    public void Reflect(Vector2 vector, Vector2 normal, Vector2 expectedResult)
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
