using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;

    const float speed = 200;
    const float smoothRotateSpeed = 400;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var v = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));

        animator.SetFloat("moveSpeed", v.sqrMagnitude);

        if (0 < v.sqrMagnitude)
        {
            v.Normalize();

            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            // 方向キーの入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * v.z + Camera.main.transform.right * v.x;

            characterController.SimpleMove(moveForward * speed * Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(moveForward.x, 0, moveForward.z)),
                smoothRotateSpeed * Time.deltaTime);
        }
    }

    public void OnCallChangeFace(string str) { }
}
