using System;
using System.Collections.Generic;
using NUnit.Framework;
using Engine;
using Engine.Internal;
using static Engine.Mathf;

[TestFixture]
public class TestCollisionDetectionAABB
{
    [Test, TestCaseSource("checkIntersectTestCases")]
    public void CheckIntersect(Rect a, Rect b, Hit? expectedHit)
    {
        Hit hit;
        bool didHit = CollisionDetectionHelper.CheckIntersect(a, b, out hit);

        if (!expectedHit.HasValue)
        {
            Assert.IsFalse(didHit);
            return;
        }

        Assert.That(didHit);
        Assert.That(hit.position, Is.EqualTo(expectedHit.Value.position));
        Assert.That(hit.delta   , Is.EqualTo(expectedHit.Value.delta   ));
        Assert.That(hit.normal  , Is.EqualTo(expectedHit.Value.normal  ));
    }

    public static TestCaseData[] checkIntersectTestCases = new[]
    {
        new TestCaseData(
            Rect.FromCenterAndHalfDiagonal(Vector2.zero, new Vector2(20f, 20f)),
            Rect.FromCenterAndHalfDiagonal(new Vector2(35f, 20f), new Vector2(20f, 20f)),
            new Hit()
            {
                normal   = new Vector2(-1f, 0f),
                delta    = new Vector2(-5f, 0f),
                position = new Vector2(15f, 0f)
            }
        ),
        #region Sides simple
        new TestCaseData(
            new Rect(0.9f, 0f, 1f, 1f),
            new Rect(0f  , 0f, 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(1f  , 0f  ),
                delta    = new Vector2(0.1f, 0f  ),
                position = new Vector2(1f  , 0.5f)
            }
        ),
        new TestCaseData(
            new Rect(-0.9f, 0f, 1f, 1f),
            new Rect(0f   , 0f, 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(-1f  , 0f  ),
                delta    = new Vector2(-0.1f, 0f  ),
                position = new Vector2( 0f  , 0.5f)
            }
        ),
        new TestCaseData(
            new Rect(0f, 0.9f, 1f, 1f),
            new Rect(0f, 0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(0f  , 1f  ),
                delta    = new Vector2(0f  , 0.1f),
                position = new Vector2(0.5f, 1f  )
            }
        ),
        new TestCaseData(
            new Rect(0f, -0.9f, 1f, 1f),
            new Rect(0f,  0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(0f  , -1f  ),
                delta    = new Vector2(0f  , -0.1f),
                position = new Vector2(0.5f,  0f  )
            }
        ),
        #endregion
        #region Sides complex
        new TestCaseData(
            new Rect(0.9f, 0.2f, 1f, 1f),
            new Rect(0f  , 0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(1f  , 0f  ),
                delta    = new Vector2(0.1f, 0f  ),
                position = new Vector2(1f  , 0.7f)
            }
        ),
        new TestCaseData(
            new Rect(-0.9f, 0.2f, 1f, 1f),
            new Rect(0f   , 0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(-1f  , 0f  ),
                delta    = new Vector2(-0.1f, 0f  ),
                position = new Vector2( 0f  , 0.7f)
            }
        ),
        new TestCaseData(
            new Rect(0.2f, 0.9f, 1f, 1f),
            new Rect(0f  , 0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(0f  , 1f  ),
                delta    = new Vector2(0f  , 0.1f),
                position = new Vector2(0.7f, 1f  )
            }
        ),
        new TestCaseData(
            new Rect(0.2f, -0.9f, 1f, 1f),
            new Rect(0f  ,  0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(0f  , -1f  ),
                delta    = new Vector2(0f  , -0.1f),
                position = new Vector2(0.7f,  0f  )
            }
        ),
        #endregion
        #region Corners
        new TestCaseData(
            new Rect(0.9f, 0.9f, 1f, 1f),
            new Rect(0f  , 0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(1f  , 1f  ),
                delta    = new Vector2(0.1f, 0.1f),
                position = new Vector2(1f  , 1f  ),
            }
        ),
        new TestCaseData(
            new Rect(-0.9f, 0.9f, 1f, 1f),
            new Rect( 0f  , 0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(-1f  , 1f  ),
                delta    = new Vector2(-0.1f, 0.1f),
                position = new Vector2(-1f  , 1f  ),
            }
        ),
        new TestCaseData(
            new Rect(0.9f, -0.9f, 1f, 1f),
            new Rect(0f  ,  0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(1f  , -1f  ),
                delta    = new Vector2(0.1f, -0.1f),
                position = new Vector2(1f  , -1f  ),
            }
        ),
        new TestCaseData(
            new Rect(-0.9f, -0.9f, 1f, 1f),
            new Rect( 0f  ,  0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(-1f  , -1f  ),
                delta    = new Vector2(-0.1f, -0.1f),
                position = new Vector2(-1f  , -1f  ),
            }
        ),
        #endregion
    };
}