using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonActionBase : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    public virtual void Activate() { }
}
