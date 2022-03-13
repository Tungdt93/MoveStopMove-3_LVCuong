using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 TargetPosition,CharacterPos;
    public float bulletSpeed=8f;
    // Start is called before the first frame update
    void Start()
    {
        CharacterPos = new Vector3(transform.position.x,1, transform.position.z);
        TargetPosition = GameObject.Find("Enemy").transform.position;
        TargetPosition.y = 1;
    }

    private void Update()
    {
        BulletMove(CharacterPos, TargetPosition);
    }
    public void BulletMove(Vector3 CharacterPos,Vector3 TargetPos)
    {
        Vector3 dirrect = TargetPos - CharacterPos;
        transform.Translate(dirrect.normalized * Time.deltaTime* bulletSpeed, Space.World);
        float angle = Vector3.Angle(dirrect, transform.forward);
        transform.LookAt(TargetPos);
    }
}
