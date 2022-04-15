using UnityEngine;
using System.Collections;

public class throwScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _speed = 2;
    private Rigidbody _rigidbody;
    private Vector3 _endPosition;
    private float _currentTime = 0;

    IEnumerator flyTime()
    {
        yield return new WaitForSeconds(1f);

        Vector3 _nextPosition;
        Vector3 _target;
        while(_currentTime < 1.5f)
        {
            _target = new Vector3(_endPosition.x, 1.8f, _endPosition.z);
            _nextPosition = Vector3.MoveTowards(_rigidbody.position, _target, Time.deltaTime * _speed);

            _rigidbody.MovePosition(_nextPosition);
            _currentTime += Time.deltaTime;

            yield return null;
        }
        _animator.SetBool("isRotate", true);

        yield return new WaitForSeconds(0.5f);

        _rigidbody.isKinematic = false;
    }

    private void deleteElements()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _endPosition = new Vector3(0.025f, 1.5f, 4f);
        if (gameObject.name.Length > 10)
        {
            EventManagerScript._event.AddListener(deleteElements);
            StartCoroutine(flyTime());
        }
    }
}