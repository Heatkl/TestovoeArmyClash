using Game.Battle;
using UnityEngine;
using UnityEngine.SceneManagement;

//Класс заглушка для выполнения условий ТЗ, с возможностью расширения функционала
public class FinishExecutor : MonoBehaviour
{
    [SerializeField] string finishSceneName = "MainMenu";
    [SerializeField] float timeBeforeRestart = 5f;
    private void OnEnable()
    {
        BattleController.OnBattleFinished += SetFinishActions;
    }

    private void OnDisable()
    {
        BattleController.OnBattleFinished -= SetFinishActions;
    }

    private void SetFinishActions()
    {
        Invoke(nameof(LoadFinishScene), timeBeforeRestart);
    }

    private void LoadFinishScene()
    {
        SceneManager.LoadScene(finishSceneName);
    }
}
