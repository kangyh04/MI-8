using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
    public float speed = 1.0f;  //透明化の速さ
    float alpha;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数

    // flags
    bool IsFadeIn = false;
    bool IsFadeOut = false;

    void Start () {
        //Panelの色を取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        //IsFadeIn = false;
        //IsFadeOut = false;
        //Debug.Log("Fade Initialize");
    }

    // Update is called once per frame
    void Update () {

        // FadeIn
        if(IsFadeIn == true)
        {
            alpha += speed * Time.deltaTime;
            if( alpha > 1.0f)
            {
                alpha = 1.0f;
                IsFadeIn = false;
            }
        }
        // FadeOut
        if(IsFadeOut == true)
        {
            alpha -= speed * Time.deltaTime;
            if(alpha < 0.0f)
            {
                alpha = 0.0f;
                IsFadeOut = false;
            }
        }
        // Set Fade
        GetComponent<Image>().color = new Color(red, green, blue, alpha);

        // debug
        //Debug.Log(alpha);
    }

    // Set FadeIn
    public void SetFadeIn()
    {
        IsFadeIn = true;
        IsFadeOut = false;
        alpha = 0.0f;

        //Debug.Log("SetFadeIn");
    }

    // Set FadeOut
    public void SetFadeOut()
    {
        IsFadeIn = false;
        IsFadeOut = true;
        alpha = 1.0f;

        //Debug.Log("SetFadeOut");
    }
}
