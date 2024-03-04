using UnityEngine;

public class PCInteractionLogic : MonoBehaviour, IInteractable
{
	[SerializeField] private UITestPassingLogic _uITestPassingLogic;
	private bool _isLocked;

	public void Interact(GameObject obj)
	{
		if (!_isLocked) {
			_uITestPassingLogic.InitialSetup(obj.GetComponent<Test>());
		}
	}

	void OnEnable()
	{
		UITestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		UITestPassingLogic.TestFailed += OnTestFailed;
		PCRenderer.FailedTimerElapsed += OnFailedTimerElapsed;
	}

	void OnDisable()
	{
		UITestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		UITestPassingLogic.TestFailed -= OnTestFailed;
		PCRenderer.FailedTimerElapsed -= OnFailedTimerElapsed;
	}

	private void OnTestSuccessfullyPassed(GameObject obj)
	{
		_isLocked = true;
	}

	private void OnTestFailed(GameObject obj)
	{
		_isLocked = true;
	}

	private void OnFailedTimerElapsed()
	{
		_isLocked = false;
	}
}