using System;
using UnityEngine;

public class PCInteractionListener : MonoBehaviour, IInteractable
{
	private bool _isLocked;
	public static event Action<GameObject> PcInteracted;

	public void Interact(GameObject obj)
	{
		Test test = obj.GetComponent<Test>();
		if (!test.IsReplayable && test.AttemptsToPassTest == 1 && test.IsIncorrect)
		{
			return;
		}
		test.AttemptsToPassTest += 1;

		if (!_isLocked && PCSideChecker.IsOnSideCheckTrigger)
		{
			var cheatMode = GetComponent<CheatMode>();
			cheatMode.enabled = true;
			cheatMode.StartCheatMode(test);

			PlayerKeyboardInteractionController.DisableInventorySystem();
			PlayerKeyboardInteractionController.DisableItemInteractionLogic();
			PlayerKeyboardInteractionController.DisableMovement();
			PlayerKeyboardInteractionController.DisableMouseLook();

			StopGameLogic.StopGame();

			var cameraMovementAnimation = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraMovementAnimation>();
			cameraMovementAnimation.enabled = true;
			cameraMovementAnimation.IsMovingTo = true;

			PcInteracted?.Invoke(obj);

			BgMusicManager.PlayBgTestMusic(1.5f);
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