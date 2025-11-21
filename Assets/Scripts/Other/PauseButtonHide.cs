using UnityEngine;

public class PauseButtonHide : MonoBehaviour
{
    private Vector3 _scale;
    private void Awake()
    {
        _scale = transform.localScale;
    }

    private void Update()
    {
        var sc = ServiceProvider.GetService<SceneController>();
        if (!sc.IsGameplaySceneActive())
            gameObject.transform.localScale = new Vector3(0,0,0);
        else
            gameObject.transform.localScale = _scale;
    }
}
