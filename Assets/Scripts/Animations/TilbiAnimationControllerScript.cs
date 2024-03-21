using System;
using UnityEngine;
using UnityEngine.AI;

public class TilbiAnimationControllerScript : MonoBehaviour {
	private Animator _animator;

	[SerializeField] private int _waveAnimationToRepeat;
	private int _currentWaveAnimationCount;

	private NavMeshAgent _navMeshAgent;
	private float _speed;

	public static Action TilbiAttacked;

	void Awake() {
		_animator = GetComponent<Animator>();
		_navMeshAgent = GetComponentInParent<NavMeshAgent>();

		if (PlayerPrefs.GetInt("PassedTests") == 0) {
			_currentWaveAnimationCount = 0;
			_animator.SetBool("IsWaving", true);
		}
	}

	void OnEnable() {
		PlayerCollisionListener.PlayerCatched += OnPlayerCatched;
	}

	void OnDisable() {
		PlayerCollisionListener.PlayerCatched -= OnPlayerCatched;
	}

	private void OnPlayerCatched() {
		_animator.SetTrigger("Attack");
	}

	void Update() {
		_speed = _navMeshAgent.speed;
		if (_speed > 0.5f) {
			_animator.SetBool("IsWalking", true);
			_animator.SetBool("IsWaving", false);
		} else {
			_animator.SetBool("IsWalking", false);
		}
	}

	private void OnWaveAnimationPlayed() {
		if (_currentWaveAnimationCount < _waveAnimationToRepeat) {
			++_currentWaveAnimationCount;
		} else {
			_animator.SetBool("IsWaving", false);
		}
	}

	private void OnTilbiAttacked() {
		TilbiAttacked?.Invoke();
	}
}