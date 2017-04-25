using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ButtonActionBase : MonoBehaviour
{
    public virtual void Inactive() { }
    public virtual void Hover() { }
    public virtual void Activate() { }
    public virtual void Disabled() { }
}
