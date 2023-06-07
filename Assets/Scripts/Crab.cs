using UnityEngine;
using TMPro;

public class Crab : MonoBehaviour
{
    [SerializeField] private BonusController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private TextMeshProUGUI _awardText;

    public TextMeshProUGUI AwardText => _awardText;
    public BoxCollider2D Collider => _collider;

    public void Appear() {
        _animator.SetTrigger("Appear");
    }

    public void Hide() {
        _animator.SetTrigger("Hide");
    }

    public void OnMouseDown() {
        _controller.SelectCrab(this);
    }
}
