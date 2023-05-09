using UnityEngine;

[RequireComponent(typeof(MovementTransform2D))]
public class MovingEntity : MonoBehaviour
{
    private MovementTransform2D movement2D;
    private Vector3 originPosition; //최초 위치
    private Vector3 originDirection;//최초 이동 방향

    private void Awake()
    {
        movement2D = GetComponent<MovementTransform2D>();
        
        originPosition = transform.position;
        originDirection = movement2D.MoveDirection;
    }

    public void Reset()
    {
        //이동 방향과 위치 초기화
        movement2D.MoveTo(originDirection);
        transform.position = originPosition;
    }
}
