using UnityEngine;

public class FragmentTest : MonoBehaviour
{
    Fragment fragment;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fragment = GetComponent<Fragment>();
        Destroy(this);
        fragment.Break();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
