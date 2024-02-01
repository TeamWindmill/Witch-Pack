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
            transform.position = Vector3.MoveTowards(transform.position, allPositions[counter], GAME_TIME.GameDeltaTime * speed);
            if (Vector3.Distance(transform.position, allPositions[counter]) < DistanceToTarget)
            {
                //Rotate((allPositions[counter] - transform.position).normalized * -1);
                counter++;
            }
        }
        else
        {
            transform.position = target.transform.position;
            setup = false;
            if (target.Damageable.CurrentHp <= 0)
            {
                OnShotHit?.Invoke(ability, owner, target);
                Disable();
            }
        }
    }
    private Vector3 CubicCurve(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
    {
        return (((-start + 3 * (control1 - control2) + end) * t + (3 * (start + control2) - 6 * control1)) * t +
                3 * (control1 - start)) * t + start;
    }

    public override void Disable()
    {
        base.Disable();
        setup = false;
        counter = 0;
    }
}
