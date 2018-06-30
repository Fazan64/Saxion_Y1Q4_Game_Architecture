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
    public void CheckIntersect(Rect mover, Rect stator, Hit? expectedHit)
    {
        Hit hit;
        bool didHit = CollisionDetectionHelper.CheckIntersect(mover, stator, out hit);

        if (!expectedHit.HasValue)
        {
            Assert.IsFalse(didHit);
            return;
        }

        Assert.That(didHit);
        Assert.That(hit.normal  , Is.EqualTo(expectedHit.Value.normal  ));
        Assert.That(hit.delta   , Is.EqualTo(expectedHit.Value.delta   ));
        Assert.That(hit.position, Is.EqualTo(expectedHit.Value.position));
    }

    public static TestCaseData[] checkIntersectTestCases = new[]
    {
        #region No penetration
        new TestCaseData(
            new Rect(-10f, 1f, 1f, 1f),
            new Rect( 0f , 0f, 1f, 1f),
            null
        ),
        new TestCaseData(
            new Rect(1f , 10f, 1f, 1f),
            new Rect(0f ,  0f, 1f, 1f),
            null
        ),
        new TestCaseData(
            new Rect(1f  , 1f, 1f, 1f),
            new Rect(0f  , 0f, 1f, 1f),
            null
        ),
        #endregion
        #region Single-axis penetration
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
        #region Non-equal penetration)
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
        #region Equal penetration
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
                position = new Vector2( 0f  , 1f  ),
            }
        ),
        new TestCaseData(
            new Rect(0.9f, -0.9f, 1f, 1f),
            new Rect(0f  ,  0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(1f  , -1f  ),
                delta    = new Vector2(0.1f, -0.1f),
                position = new Vector2(1f  ,  0f  ),
            }
        ),
        new TestCaseData(
            new Rect(-0.9f, -0.9f, 1f, 1f),
            new Rect( 0f  ,  0f  , 1f, 1f),
            new Hit()
            {
                normal   = new Vector2(-1f  , -1f  ),
                delta    = new Vector2(-0.1f, -0.1f),
                position = new Vector2( 0f  ,  0f  ),
            }
        ),
        #endregion
    };
}