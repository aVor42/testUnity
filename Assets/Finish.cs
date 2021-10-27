using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    private bool _isActivated = false;
    private int _testGitVariable = 42;

    public void Activate() =>
        _isActivated = true;
    

    public void FinishLevel()
    {
        if (_isActivated && _testGitVariable > 0)
        {
            gameObject.SetActive(false);
            ++_testGitVariable;
        }
    }
}
