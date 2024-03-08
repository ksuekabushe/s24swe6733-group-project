using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameLogicTests
{
    PlayerMovementRestrictions _playerMovementRestrictions = new PlayerMovementRestrictions(-10f, 10f, -10f, 10f, -10f, 10f);
    Vector3 _maxVector = new Vector3(10f, 10f, 10f);
    Vector3 _minVector = new Vector3(-10f, -10f, -10f);

    #region Move Tests

    [UnityTest]
    public IEnumerator MoveUpTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_UP, _playerMovementRestrictions );

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(new Vector3(0f,0f + player.MoveDistance, 0f), gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveDownTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_DOWN, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(new Vector3(0f, 0f - player.MoveDistance, 0f), gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveRightTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_RIGHT, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(new Vector3(0f + player.MoveDistance, 0f, 0f), gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveLeftTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_LEFT, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(new Vector3(0f - player.MoveDistance, 0f, 0f), gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveForwardTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_FRONT, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(new Vector3(0f, 0f, 0f + player.MoveDistance), gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveBackTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_BACK, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(new Vector3(0f, 0f, 0f - player.MoveDistance), gameObject.transform.position);
    }
    #endregion

    #region Limit Tests
    [UnityTest]
    public IEnumerator MoveUpLimitTest()
    {
        var gameObject = new GameObject();
        gameObject.transform.position = _maxVector;
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_UP, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(_maxVector, gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveDownLimitTest()
    {
        var gameObject = new GameObject();
        gameObject.transform.position = _minVector;
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_DOWN, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(_minVector, gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveRightLimitTest()
    {
        var gameObject = new GameObject();
        gameObject.transform.position = _maxVector;
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_RIGHT, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(_maxVector, gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveLeftLimitTest()
    {
        var gameObject = new GameObject();
        gameObject.transform.position = _minVector;
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_LEFT, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(_minVector, gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveForwardLimitTest()
    {
        var gameObject = new GameObject();
        gameObject.transform.position = _maxVector;
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_FRONT, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(_maxVector, gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveBackLimitTest()
    {
        var gameObject = new GameObject();
        gameObject.transform.position = _minVector;
        var player = gameObject.AddComponent<GameLogic>();
        GameLogic.movePlayer(player.transform, RelativeMovementDirection.MOVE_BACK, _playerMovementRestrictions);

        yield return new WaitForSeconds(player.MoveDuration);

        Assert.Equals(_minVector, gameObject.transform.position);
    }
    #endregion

}
