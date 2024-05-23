using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    float yOffset;//y���W�̏����ʒu���i�[
    float zOffset;//x���W�̏����ʒu���i�[

    const float X_RATIO = 0.7f;//���̒��S�ƃv���C���[�̊Ԃ̐�����̉����ڂɔz�u���邩

    // Start is called before the first frame update
    private void Start()
    {
        yOffset = transform.localPosition.y;
        zOffset = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        //�e���W�̌v�Z
        var x = player.transform.localPosition.x * X_RATIO;
        var y = player.transform.localPosition.y + yOffset;
        var z = player.transform.localPosition.z + zOffset;

        //�ڕW�n�_�̈ʒu�x�N�g������
        Vector3 newLocalpos = new Vector3(x, y, z);

        //�ڕW�n�_�ւ������ړ�������
        transform.localPosition = Vector3.Lerp(transform.localPosition, newLocalpos, 0.2f);
    }
}
