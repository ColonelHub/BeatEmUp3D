using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private List<ComboData> combosDataList;

    private AnimatorOverrideController _animatorOverrideController;
    private AnimationEventsHandler _animationEventsHandler;
    private Animator _animator;

    [SerializeField] private List<AttackType> registeredAttacks = new List<AttackType>();
    private const int maxAttacks = 4;

    private AnimationClip _defaultAttackClip;
    private AnimationClip _comboClip;

    private const string PUNCH_KEY = "Punch1";

    private Coroutine _resetComboCoroutine;

    [Inject]
    private void Construct(Player player)
    {
        _animator = player.Animator;
        _animationEventsHandler = _animator.GetComponent<AnimationEventsHandler>();
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;

        _animationEventsHandler.OnComboEnded += OnComboEnded;
        _defaultAttackClip = _animatorOverrideController[PUNCH_KEY];
    }

    [System.Serializable]
    public struct ComboData
    {
        public AnimationClip AnimationClip;
        public int DamageAmount;

        [Space]
        public List<AttackType> AttacksSequence;
    }

    public void RegisterAttack(AttackType attackType)
    {
        registeredAttacks.Add(attackType);

        if (registeredAttacks.Count > maxAttacks)
        {
            registeredAttacks.RemoveAt(0);
        }

        StopAllCoroutines();
        _resetComboCoroutine = StartCoroutine(ResetComboCoroutine());
    }

    public bool CanUseCombo()
    {
        foreach (var comboData in combosDataList)
        {
            if (registeredAttacks.Count >= comboData.AttacksSequence.Count)
            {
                bool isComboValid = true;
                for (int i = 0; i < comboData.AttacksSequence.Count; i++)
                {
                    if (comboData.AttacksSequence[comboData.AttacksSequence.Count - 1 - i] !=
                        registeredAttacks[registeredAttacks.Count - 1 - i])
                    {
                        isComboValid = false;
                        break;
                    }
                }

                if (isComboValid)
                {
                    _comboClip = comboData.AnimationClip;
                    registeredAttacks.Clear();
                    StopCoroutine(_resetComboCoroutine);
                    return true;
                }
            }
        }
        return false;
    }

    public void UseCombo()
    {
        _animatorOverrideController[PUNCH_KEY] = _comboClip;
        _animator.SetTrigger("Punch");
    }

    private void OnComboEnded()
    {
        _animatorOverrideController[PUNCH_KEY] = _defaultAttackClip;
        _comboClip = null;
    }

    private void ResetCombo()
    {
        registeredAttacks.Clear();
        _animatorOverrideController[PUNCH_KEY] = _defaultAttackClip;
        _comboClip = null;
    }

    private IEnumerator ResetComboCoroutine()
    {
        yield return new WaitForSeconds(2f);
        ResetCombo();
    }

    private void OnDisable()
    {
        _animationEventsHandler.OnComboEnded -= OnComboEnded;
    }
}