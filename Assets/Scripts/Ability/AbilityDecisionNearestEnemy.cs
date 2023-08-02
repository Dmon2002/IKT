using Sirenix.OdinInspector;
using StatSystem;
using UnityEngine;

public class AbilityDecisionNearestEnemy : AbilityDecisionDirection
{
    [SerializeField] private Team _ourTeam;

    [ShowInInspector]
    public bool Active => this.enabled;

    // „тобы не засор€ть инспектор, решил вынести константу
    private LayerMask _detectMask;

    private void Start()
    {
        // Ќельз€ получать маски при инициализации, поэтому присвоил в Start
        _detectMask = LayerMask.GetMask("Entity", "Player");
    }

    public override Vector2 DecideDirection()
    {
        //  огда LayerMask'и в проекте более менее закреп€тс€, можно будет оптимизировать
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
