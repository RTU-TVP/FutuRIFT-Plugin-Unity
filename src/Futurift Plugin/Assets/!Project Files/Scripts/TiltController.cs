using UnityEngine;

public class TiltController : MonoBehaviour
{
    [Header("Скорость наклона")] public float tiltSpeed = 30f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(
            -vertical * tiltSpeed * Time.deltaTime, 0f, -horizontal * tiltSpeed * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnGUI()
    {
        GUI.Label(
            new Rect(10, 10, 600, 20),
            "Текущий угол поворота (Euler): " + transform.eulerAngles
        );
    }
}