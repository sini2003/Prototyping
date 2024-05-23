using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public float mainSPEED;
    public float x_sensi;
    public float y_sensi;
    public new GameObject camera;
    public GameObject AimUi,GanSetOb;
    [SerializeField] GameObject Gan;
    Vector3 FirstPos, GanSetPos;
    Quaternion FirstQuo;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        FirstPos = Gan.transform.localPosition;
        FirstQuo = Gan.transform.localRotation;
        GanSetPos = GanSetOb.transform.localPosition;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShotGameManager.GameOnOff == true && Input.GetMouseButton(1))
        {
            CameraCon();
            Cursor.visible = false;
            AimUi.SetActive(true);
            Gan.transform.localPosition = GanSetPos;
            cam.fieldOfView = 25;
        }
        else
        {
            Cursor.visible = true;
            AimUi.SetActive(false);
            this.transform.Rotate(0, 0, 0);
            camera.transform.Rotate(-0, 0, 0);
            Gan.transform.localPosition = FirstPos;
            Gan.transform.localRotation = FirstQuo;
            cam.fieldOfView = 60;
        }
    }
    void CameraCon()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        x_Rotation = x_Rotation * x_sensi;
        y_Rotation = y_Rotation * y_sensi;
        this.transform.Rotate(0, x_Rotation, 0);
        camera.transform.Rotate(-y_Rotation, 0, 0);
    }
}
