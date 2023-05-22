using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private StatContainer _statContainer;

    public StatContainer StatContainer => _statContainer;

    // ���� �������� ��������
    public abstract void Strike(Vector2 direction);
    
}
