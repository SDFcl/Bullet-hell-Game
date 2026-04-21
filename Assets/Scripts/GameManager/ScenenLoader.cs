using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    protected override bool UseDontDestroyOnLoad => false;

    private bool isLoading;

    public void LoadScene(string sceneName, GameObject source)
    {
        if (isLoading)
            return;

        StartCoroutine(LoadSceneRoutine(sceneName, source));
    }

    public void ReloadCurrentScene(GameObject source)
    {
        LoadScene(SceneManager.GetActiveScene().name, source);
    }

    private IEnumerator LoadSceneRoutine(string sceneName, GameObject source)
    {
        isLoading = true;

        List<ISceneExitTask> tasks = new List<ISceneExitTask>();

        if (source != null)
            tasks.AddRange(source.GetComponents<ISceneExitTask>());

        tasks.AddRange(FindGlobalExitTasks());

        yield return RunTasksParallel(tasks);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerable<ISceneExitTask> FindGlobalExitTasks()
    {
        GlobalSceneExitTaskMarker[] markers =
        FindObjectsByType<GlobalSceneExitTaskMarker>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);


        foreach (GlobalSceneExitTaskMarker marker in markers)
        {
            foreach (MonoBehaviour behaviour in marker.GetComponents<MonoBehaviour>())
            {
                if (behaviour is ISceneExitTask task)
                    yield return task;
            }
        }
    }

    private IEnumerator RunTasksParallel(List<ISceneExitTask> tasks)
    {
        int remaining = tasks.Count;

        if (remaining == 0)
            yield break;

        foreach (ISceneExitTask task in tasks)
            StartCoroutine(RunTask(task, () => remaining--));

        while (remaining > 0)
            yield return null;
    }

    private IEnumerator RunTask(ISceneExitTask task, Action onComplete)
    {
        yield return task.Execute();
        onComplete?.Invoke();
    }
}
