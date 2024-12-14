using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager2 : MonoBehaviour
{
    public int width;
    public int height;
  public void SetWidth2(int newWidth)
  {
    width = newWidth;
  }
  public void SetHeight2(int newHeight)
  {
    height = newHeight;
  }
  public void SetRes()
  {
    Screen.SetResolution(width, height, true);
  }
}
