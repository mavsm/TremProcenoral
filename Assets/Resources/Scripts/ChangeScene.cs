using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] int targetSceneIndex;

    public void OnChangeScene(){
        SceneManager.LoadScene(targetSceneIndex);
    }
}
