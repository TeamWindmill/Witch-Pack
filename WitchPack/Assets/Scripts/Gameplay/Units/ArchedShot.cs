using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchedShot : TargetedShot
{
    [SerializeField] private int numOfPoint;
    private Vector3 offset;
    private const float DistanceToTarget = 1;

    private Vector3 initialPosition;
    private List<Vector3> allPositions;
    private bool setup;
    private int counter = 0;
    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir, BaseUnit target, Vector3 archSize)
    {
        owner = shooter;
        ability = givenAbility;
        this.target = target;
        offset = archSize;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            offset = new Vector2(0, offset.y * 2);
        }
        else
        {
            offset = new Vector2(offset.x * 2, 0);
        }
        Rotate(dir);
        Init();
    }

    private void Init()
    {
        initialPosition = transform.position;
        allPositions = new List<Vector3>(numOfPoint);
        setup = true;

        for (var i = 0; i < numOfPoint; i++)
        {
            var newPosition = CubicCurve(initialPosition, initialPosition + offset, initialPosition + offset,
                target.transform.position, (float)i / numOfPoint);
            allPositions.Add(newPosition);
        }
        setup = true;
    }

    private void Update()
    {
        if (!setup)
        {
            return;
        }
        if (counter < allPositions.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, allPositions[counter], Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, allPositions[counter]) < DistanceToTarget)
            {
                counter++;
            }
        }
        else
        {
            if (target.Damageable.CurrentHp > 0)
            {
                transform.position = target.transform.position;
            }
            else
            {
                Disable();
            }
        }
    }
    private Vector3 CubicCurve(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
    {
        return (((-start + 3 * (control1 - control2) + end) * t + (3 * (start + control2) - 6 * control1)) * t +
                3 * (control1 - start)) * t + start;
    }

    private IEnumerator FixedDirMovement(Vector3 dir)
    {
        float lerpCounter = 0f;
        Vector2 dest = transform.position + (dir * 1.5f);
        Vector3 startPos = transform.position;
        while (lerpCounter < 1)
        {
            Vector3 lerpedPos = Vector3.Lerp(startPos, dest, lerpCounter);
            transform.position = lerpedPos;
            lerpCounter += GAME_TIME.GameDeltaTime * 5f;
            yield return new WaitForEndOfFrame();
        }
        Init();
    }

    protected override void Disable()
    {
        base.Disable();
        setup = false;
        counter = 0;
    }
}
