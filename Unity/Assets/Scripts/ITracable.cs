using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITracable
{
    /// <summary>
    /// Runs when the object is clicked on.
    /// </summary>
    void OnSelect();

    /// <summary>
    /// Runs when the object is hovered over by the mouse.
    /// </summary>
    void OnHover();
}
