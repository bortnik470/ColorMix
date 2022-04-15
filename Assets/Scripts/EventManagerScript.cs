using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventManagerScript : MonoBehaviour
{
    [SerializeField] private ColorMixScript _colorMix;
    [SerializeField] private Animator _lidAnimator;
    [SerializeField] private Animator _blenderAnimator;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject[] _gameObject;
    private int _currentLevel = 0;

    private bool _stop = false;

    public static UnityEvent _event = new UnityEvent();
    public static UnityAction _newLevel;

    private IEnumerator mixTime()
    {
        yield return new WaitForSeconds(1);
        _blenderAnimator.SetBool("_isWork", true);
        yield return new WaitForSeconds(2);
        _blenderAnimator.SetBool("_isWork", false);
        _event.Invoke();
        if (_newLevel != null)
        {
            yield return new WaitForSeconds(1);
            openMenu();
        }
    }

    private IEnumerator flyTime()
    {
        yield return new WaitForSeconds(4);
        _stop = false;
    }

    public void throwColor(string _fructName)
    {
        if (_stop == true)
            return;
        _stop = true;
        StartCoroutine(flyTime());
        Vector3 _position = new Vector3(0, 1.2f, 5f);
        _lidAnimator.SetBool("_isOpen", true);
        foreach (GameObject c in _gameObject)
        {
            if(c.name == _fructName)
            {
                _colorMix._colors.Add(c.GetComponent<MeshRenderer>().materials[0].color);
                Instantiate(c, _position, new Quaternion());
                break;
            }
        }
    }

    private void openMenu()
    {
        _pauseMenu.SetActive(true);
    }

    public void nextLevel(int _levelNumber)
    {
        _levels[_currentLevel].SetActive(false);
        _currentLevel = _levelNumber;
        _levels[_levelNumber].SetActive(true);
        _gameObject = GameObject.FindGameObjectsWithTag("Fruct");
        foreach (GameObject c in _gameObject)
            _colorMix._colors.Add(c.GetComponent<MeshRenderer>().materials[0].color);
        _colorMix.newLevel();
        _pauseMenu.SetActive(false);
    }

    public void startMix()
    {
        _lidAnimator.SetBool("_isOpen", false);
        StartCoroutine(mixTime());
    }

    public void exit()
    {
        Application.Quit();
    }

    private void Start()
    {
        openMenu();
    }
}
