using System.Collections.Generic;
using UnityEngine;


public class EButton : MonoBehaviour
{
    public Element element;

    private EBook EBook;

    private void Start()
    {
        EBook = FindObjectOfType<EBook>(); // 获取MagicBook组件的引用
    }

    public void OnElementSelected()
    {
        Debug.Log("Element selected: " + element.name);
        EBook.AddElementToHand(element);
    }
}
