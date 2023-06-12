using UnityEngine;

public class AbilityDecisionNearestEnemy : AbilityDirectionDecision
{
    [SerializeField] private Team _ourTeam;

    // ����� �� �������� ���������, ����� ������� ���������
    private LayerMask _detectMask;

    private void Start()
    {
        // ������ �������� ����� ��� �������������, ������� �������� � Start
        _detectMask = LayerMask.GetMask("Entity", "Player");
    }

    public override Vector2 DecideDirection()
    {
        // ����� LayerMask'� � ������� ����� ����� ����������, ����� ����� ��������������
        var cols = Physics2D.OverlapCircleAll(transform.position, Ability.StatContainer.GetStatFloatValue(Stat.AbilityAttackRadiusName), _detectMask);

        Entity minEntity = null;
        float minDist = float.PositiveInfinity;
        foreach (var col in cols)
        {
            if (col.TryGetComponent<Entity>(out var entity))
            {
                Team team = (Team)entity.StatContainer.GetStatEnumValue(Stat.TeamStatName);
                if (team.GetAgainstTeams().Contains(_ourTeam))
                {
                    float dist = Vector2.Distance(transform.position, entity.transform.position);
                    if (dist < minDist)
                    {
                        minEntity = entity;
                        minDist = dist;
                    }
                }
            }
        }
        if (minEntity == null)
            return Vector2.zero;
        return (minEntity.transform.position - transform.position).normalized;
    }
}
