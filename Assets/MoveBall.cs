using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public int distance = 10;

    void Update()
    {
        Vector3 position = transform.position;

        // 不能同時發生
        // if () {
        //     position.y += 0.1f;
        // }
        // if () {
        //     position.x += 0.1f;
        // }
        // if () {
        //     position.y -= 0.1f;
        // }
        // if () {
        //     position.x -= 0.1f;
        // }
        //

        int a = 1;
        if (a == 1) { // true, false
            // 等於一
        }

        if (a == 1) {
            // 等於一
        } else {
            // 不等於一
        }


        if (a == 1) {
            // 等於一
        } else if (a > 1) {
            // 大於一
        } else {
            // 不等於一
        }
        

        transform.position = position;


        // switch // 自己去查, 加油, 老師說的

        switch (a) {
            case 1:
                Debug.Log(1);
                break;
            case 2:
                Debug.Log(2);
                break;
        }

        // 上下這兩行會有同樣的結果

        if (a == 1) {
            Debug.Log(1);
        } else if (a == 2) {
            Debug.Log(2);
        }






        if (false) { // 啦啦啦 我要做很多測試
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
            Debug.Log("啦啦啦 我想要測試");
        }




        


        // position.y += 0.1f;

        // transform.position = position;


        // transform.position = new Vector3(0, 10, 0);
        // transform.position = new Vector3(10, 10, 0);
        // transform.position = new Vector3(10, 0, 0);
        // transform.position = new Vector3(0, 0, 0);

        // Vector2 vec_2d = new Vector2(0, 0);
        // Vector3 vec_3d = new Vector3(0, 0, 0);
    }
}
