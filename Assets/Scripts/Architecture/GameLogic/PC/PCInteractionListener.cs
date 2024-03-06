using System;
using UnityEngine;

public class PCInteractionListener : MonoBehaviour, IInteractable
{
	private bool _isLocked;

	public static event Action<GameObject> PcInteracted;

	public void Interact(GameObject obj)
	{
		if (!_isLocked) {
			PcInteracted?.Invoke(obj);
		}
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed += OnTestFailed;
		PCRenderer.FailedTimerElapsed += OnFailedTimerElapsed;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed -= OnTestFailed;
		PCRenderer.FailedTimerElapsed -= OnFailedTimerElapsed;
	}

	private void OnTestSuccessfullyPassed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		_isLocked = true;
	}

	private void OnTestFailed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		_isLocked = true;
	}

	private void OnFailedTimerElapsed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		_isLocked = false;
	}
}