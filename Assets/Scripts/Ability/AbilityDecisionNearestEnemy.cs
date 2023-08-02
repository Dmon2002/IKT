using Sirenix.OdinInspector;
using StatSystem;
using UnityEngine;

public class AbilityDecisionNearestEnemy : AbilityDecisionDirection
{
    [SerializeField] private Team _ourTeam;

    [ShowInInspector]
    public bool Active => this.enabled;

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
        var cols = Physics2D.OverlapCircleAll(transform.position, Ability.StatContainer.GetStat<float>(StatNames.AbilityAttackRadius), _detectMask);

        Entity minEntity = null;
        float minDist = float.PositiveInfinity;
        foreach (var col in cols)
        {
            if (col.TryGetComponent<Entity>(out var entity))
            {
                Team team = (Team)entity.StatContainer.GetStat<Team>(StatNames.Team);
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
