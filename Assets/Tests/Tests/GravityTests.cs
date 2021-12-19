using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GravityTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GravityTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    private Gravity gravity;

    [UnityTest]
    public IEnumerator GravityMovingRight()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.


        GameObject gameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        gravity = gameObject.GetComponent<Gravity>();

        gravity.test_right = true;

        float initialXPos = gameObject.transform.position.x;

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(gameObject.transform.position.x, initialXPos);
    }

    [UnityTest]
    public IEnumerator GravityMovingLeft()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.


        GameObject gameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        gravity = gameObject.GetComponent<Gravity>();

        gravity.test_left = true;

        float initialXPos = gameObject.transform.position.x;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(gameObject.transform.position.x, initialXPos);
    }

    [UnityTest]
    public IEnumerator GravityJumping()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.


        GameObject gameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        gravity = gameObject.GetComponent<Gravity>();

        gravity.test_up = true;

        float initialYPos = gameObject.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(gameObject.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GravityCollision()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.


        GameObject gameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/player"));

        GameObject ground =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/ground"));
        gravity = gameObject.GetComponent<Gravity>();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(gravity.on_ground);

        gravity.test_up = true;

        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(gravity.on_ground);
    }

}
