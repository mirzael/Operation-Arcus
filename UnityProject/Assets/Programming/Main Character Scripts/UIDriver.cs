using UnityEngine;
using MainCharacter;
using UnityEngine.UI;

public class UIDriver : MonoBehaviour
{
    public OnArcusColorBar barCenter;
    public OnArcusColorBar barLeft;
    public OnArcusColorBar barRight;

    private ShipColor currentColor; // 1 = red; 2 = blue; 3 = yellow;
    //public enum ShipColor{BLUE, RED, YELLOW, ORANGE, GREEN, PURPLE, RAINBOW}

    public Color Orange;
    public Color Purple;
    public Color Green;

    public GameObject primaryRing;
    public GameObject secondaryRing1;
    public GameObject secondaryRing2;

    protected void Awake()
    {
        currentColor = ShipColor.RED;
    }

    public void Start()
    {
        UpdateBars();
    }

    public void RotateToBlue()
    {
        if (currentColor == ShipColor.RED)
        {
            RotateLeft();

        }
        else if (currentColor == ShipColor.YELLOW)
        {
            RotateRight();
        }

        currentColor = ShipColor.BLUE;
        UpdateBars();
    }

    public void RotateToRed()
    {
        if (currentColor == ShipColor.BLUE)
        {
            RotateRight();
        }
        else if (currentColor == ShipColor.YELLOW)
        {
            RotateLeft();
        }

        currentColor = ShipColor.RED;
        UpdateBars();
    }

    public void RotateToYellow()
    {
        if (currentColor == ShipColor.RED)
        {
            RotateRight();
        }
        else if (currentColor == ShipColor.BLUE)
        {
            RotateLeft();
        }

        currentColor = ShipColor.YELLOW;
        UpdateBars();
    }

    private void RotateLeft()
    {
        Debug.Log("rotate to the left to the left");
        var tmp = barLeft.currentColor;
        barLeft.currentColor = barRight.currentColor;
        barRight.currentColor = barCenter.currentColor;
        barCenter.currentColor = tmp;
    }

    private void RotateRight()
    {
        Debug.Log("rotate to the right yeah");

        var tmp = barRight.currentColor;
        barRight.currentColor = barLeft.currentColor;
        barLeft.currentColor = barCenter.currentColor;
        barCenter.currentColor = tmp;
    }

    /* CurrentColor = 1
     * 	Left = Secondary 1 = Yellow
     * 	Center = Red
     *	Right = Secondary 2 = Blue 
     * CurrentColor = 2
     * 	Left = Secondary 1 = Red
     *  Center = Blue
     *  Right = Secondary 2 = Yellow
     * CurrentColor = 3
     *  Left = Secondary 1 = Blue
     *  Center = Yellow
     *  Right = Secondary 2 = Red 
     */

    public void UpdateBars()
    {

        //BlindBar.Instance.UpdateColorBars();

        if (currentColor == ShipColor.RED)
        {
            barLeft.UpdatePercentage(ColorPower.Instance.powerYellow);
            barCenter.UpdatePercentage(ColorPower.Instance.powerRed);
            barRight.UpdatePercentage(ColorPower.Instance.powerBlue);
        }
        else if (currentColor == ShipColor.BLUE)
        {
            barLeft.UpdatePercentage(ColorPower.Instance.powerRed);
            barCenter.UpdatePercentage(ColorPower.Instance.powerBlue);
            barRight.UpdatePercentage(ColorPower.Instance.powerYellow);
        }
        else if (currentColor == ShipColor.YELLOW)
        {
            barLeft.UpdatePercentage(ColorPower.Instance.powerBlue);
            barCenter.UpdatePercentage(ColorPower.Instance.powerYellow);
            barRight.UpdatePercentage(ColorPower.Instance.powerRed);
        }
        else
        {
            barLeft.UpdatePercentage(ColorPower.Instance.powerYellow);
            barCenter.UpdatePercentage(ColorPower.Instance.powerRed);
            barRight.UpdatePercentage(ColorPower.Instance.powerBlue);
        }

        float transformAmount = MainCharacterDriver.TRANSFORM_AMOUNT;
        if (ColorPower.Instance.powerRed >= transformAmount && ColorPower.Instance.powerBlue >= transformAmount)
        {
            if (primaryRing.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Purple)
            {
                primaryRing.renderer.material.color = Purple;
            }
            else if (secondaryRing1.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Purple)
            {
                secondaryRing1.renderer.material.color = Purple;
            }
            else
            {
                secondaryRing2.renderer.material.color = Purple;
            }
        }
        else
        {
            if (primaryRing.renderer.material.color == Purple)
            {
                var tmp = primaryRing.renderer.material.color;
                tmp.a = 0;
                primaryRing.renderer.material.color = tmp;
            }
            else if (secondaryRing1.renderer.material.color == Purple)
            {
                var tmp = secondaryRing1.renderer.material.color;
                tmp.a = 0;
                secondaryRing1.renderer.material.color = tmp;
            }
            else if (secondaryRing2.renderer.material.color == Purple)
            {
                var tmp = secondaryRing2.renderer.material.color;
                tmp.a = 0;
                secondaryRing2.renderer.material.color = tmp;
            }
        }
        if (ColorPower.Instance.powerRed >= transformAmount && ColorPower.Instance.powerYellow >= transformAmount)
        {
            if (primaryRing.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Orange)
            {
                primaryRing.renderer.material.color = Orange;
            }
            else if (secondaryRing1.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Orange)
            {
                secondaryRing1.renderer.material.color = Orange;
            }
            else
            {
                secondaryRing2.renderer.material.color = Orange;
            }
        }
        else
        {
            if (primaryRing.renderer.material.color == Orange)
            {
                var tmp = primaryRing.renderer.material.color;
                tmp.a = 0;
                primaryRing.renderer.material.color = tmp;
            }
            else if (secondaryRing1.renderer.material.color == Orange)
            {
                var tmp = secondaryRing1.renderer.material.color;
                tmp.a = 0;
                secondaryRing1.renderer.material.color = tmp;
            }
            else if (secondaryRing2.renderer.material.color == Orange)
            {
                var tmp = secondaryRing2.renderer.material.color;
                tmp.a = 0;
                secondaryRing2.renderer.material.color = tmp;
            }
        }
        if (ColorPower.Instance.powerYellow >= transformAmount && ColorPower.Instance.powerBlue >= transformAmount)
        {
            if (primaryRing.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Green)
            {
                primaryRing.renderer.material.color = Green;
            }
            else if (secondaryRing1.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Green)
            {
                secondaryRing1.renderer.material.color = Green;
            }
            else
            {
                secondaryRing2.renderer.material.color = Green;
            }
        }
        else
        {
            if (primaryRing.renderer.material.color == Green)
            {
                var tmp = primaryRing.renderer.material.color;
                tmp.a = 0;
                primaryRing.renderer.material.color = tmp;
            }
            else if (secondaryRing1.renderer.material.color == Green)
            {
                var tmp = secondaryRing1.renderer.material.color;
                tmp.a = 0;
                secondaryRing1.renderer.material.color = tmp;
            }
            else if (secondaryRing2.renderer.material.color == Green)
            {
                var tmp = secondaryRing2.renderer.material.color;
                tmp.a = 0;
                secondaryRing2.renderer.material.color = tmp;
            }
        }
    }

    /// <summary>
    /// Resize bar with relation to its original scale
    /// </summary>
    /// <param name="powerBar"></param>
    /// <param name="origScale"></param>
    /// <param name="newScaleRatio"></param>
/*    private void ShiftAndScale(GameObject powerBar, Vector3 origScale, Vector3 newScaleRatio)
    {
        Vector3 curScale = powerBar.transform.localScale;
        Vector3 newScale = new Vector3(origScale.x * newScaleRatio.x, origScale.y * newScaleRatio.y, origScale.z * newScaleRatio.z);

        if (newScale.x == curScale.x && newScale.y == curScale.y && newScale.z == curScale.z)
        {
            return;
        }

        float newX = powerBar.transform.position.x - powerBar.renderer.bounds.extents.x;
        float curY = powerBar.transform.position.y;
        float curZ = powerBar.transform.position.z;
        powerBar.transform.position = new Vector3(newX, curY, curZ);
        powerBar.transform.localScale = newScale;
        newX = powerBar.transform.position.x + powerBar.renderer.bounds.extents.x;
        powerBar.transform.position = new Vector3(newX, curY, curZ);
    }*/

    public void ShowLoseScreen()
    {
        Camera.main.gameObject.GetComponent<BackgroundUI>().ShowLoseScreen();
    }

}
