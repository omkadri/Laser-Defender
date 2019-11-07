using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollingSpeed = 0.5f;
    Material myMaterial;
    Vector2 offset;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material; //gets the material we are interested in
        offset = new Vector2(0f, backgroundScrollingSpeed);// creates the offset value in the form of a vector
    }

    private void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;//adds the offset value to the current vector value
    }
}
