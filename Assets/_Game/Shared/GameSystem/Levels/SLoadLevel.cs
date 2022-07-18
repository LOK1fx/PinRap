using UnityEngine;

public class SLoadLevel : MonoBehaviour
{
    [SerializeField]
    private LevelData _data;
    [SerializeField]
    private Animator _animator; 
    public void Load()
    {
        _animator.SetTrigger("Play");
        Invoke("Loading" , 1);
    }
    private void Loading()
    {
        SilentlyLoadLevel.LoadScenes(_data);
    }
}
