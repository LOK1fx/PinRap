using UnityEngine;

public abstract class BaseTester : MonoBehaviour
{
    [SerializeField] private KeyCode _testKey1;
    [SerializeField] private KeyCode _testKey2;
    [SerializeField] private KeyCode _testKey3;

    private void LateUpdate()
    {
        if(Input.GetKeyDown(_testKey1))
        {
            Test1();
        }
        if (Input.GetKeyDown(_testKey2))
        {
            Test2();
        }
        if (Input.GetKeyDown(_testKey3))
        {
            Test3();
        }
    }

    public abstract void Test1();
    public abstract void Test2();
    public abstract void Test3();
}