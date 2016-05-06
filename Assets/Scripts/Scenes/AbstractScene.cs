using UnityEngine;
using System;
using System.Collections;

abstract public class AbstractScene {
    // Sort of like Start
    virtual public void Setting() {

    }

    // Sort of like Update
    virtual public IEnumerator Plot() {
        yield return null;
    }

    // Why do I so often end up feeling like I am recreating the wheel?
}
