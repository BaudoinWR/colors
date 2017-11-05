using UnityEngine;
using System.Collections;

public class ColorPicker {

    public static float[] getRGB(float wavelength)
    {
        float Gamma = 0.80f;
        float IntensityMax = 255;
        float factor, red, green, blue;
        if ((wavelength >= 380) && (wavelength < 440))
        {
            red = -(wavelength - 440) / (440 - 380);
            green = 0.0f;
            blue = 1.0f;
        }
        else if ((wavelength >= 440) && (wavelength < 490))
        {
            red = 0.0f;
            green = (wavelength - 440) / (490 - 440);
            blue = 1.0f;
        }
        else if ((wavelength >= 490) && (wavelength < 510))
        {
            red = 0.0f;
            green = 1.0f;
            blue = -(wavelength - 510) / (510 - 490);
        }
        else if ((wavelength >= 510) && (wavelength < 580))
        {
            red = (wavelength - 510) / (580 - 510);
            green = 1.0f;
            blue = 0.0f;
        }
        else if ((wavelength >= 580) && (wavelength < 645))
        {
            red = 1.0f;
            green = -(wavelength - 645) / (645 - 580);
            blue = 0.0f;
        }
        else if ((wavelength >= 645) && (wavelength < 781))
        {
            red = 1.0f;
            green = 0.0f;
            blue = 0.0f;
        }
        else
        {
            red = 0.0f;
            green = 0.0f;
            blue = 0.0f;
        };
      // Let the intensity fall off near the vision limits
        if ((wavelength >= 380) && (wavelength < 420))
        {
            factor = 0.3f + 0.7f * (wavelength - 380) / (420 - 380);
        }
        else if ((wavelength >= 420) && (wavelength < 701))
        {
            factor = 1.0f;
        }
        else if ((wavelength >= 701) && (wavelength < 781))
        {
            factor = 0.3f + 0.7f * (780 - wavelength) / (780 - 700);
        }
        else
        {
            factor = 0.0f;
        };
        if (red != 0f)
        {
            red = Mathf.Round(IntensityMax * Mathf.Pow(red * factor, Gamma));
        }
        if (green != 0f)
        {
            green = Mathf.Round(IntensityMax * Mathf.Pow(green * factor, Gamma));
        }
        if (blue != 0f)
        {
            blue = Mathf.Round(IntensityMax * Mathf.Pow(blue * factor, Gamma));
        }
        return new float[] { red, green, blue };
    }



    private static float mapFloat(float init, float minSource, float maxSource, float minDest, float maxDest)
    {
        return (init - minSource) / (maxSource - minSource) * (maxDest - minDest) + minDest;
    }

    /* Map period of movement between to color wavelength
    between 380 and 780 nm*/
    private static float convertToWavelength(float period)
    {
        return mapFloat(period, 0.055f, 1, 380, 780);
    }

    private static float flattenRgb(float rgb)
    {
        return mapFloat(rgb, 0, 255, 0, 1);
    }

    public static Color GetColor(float period)
    {
        float[] rgb = ColorPicker.getRGB(convertToWavelength(period));
        // Debug.Log("[" + rgb[0] + " , " + rgb[1] + " , " + rgb[2] + "]");
        Color col = new Color(flattenRgb(rgb[0]),
                                flattenRgb(rgb[1]),
                                flattenRgb(rgb[2]));
        return col;
    }

    public static Color generateNewColor()
    {
        float period = Random.Range(0.055f, 1);
        Color col = ColorPicker.GetColor(period);
        return col;
    }

    /*
        toHex: function(number)
        {
            //converts a decimal number into hex format
            var hex = number.toString(16);
            if (hex.length < 2)
            {
                hex = "0" + hex;
            }
            return hex;
        },
            rgbToHex: function(color)
        {
            //takes an 3 element array (r,g,b) and returns a hexadecimal color
            var hexString = '#';
            for (var i = 0; i < 3; i++)
            {
                hexString += this.toHex(color[i]);
            }
            return hexString;
        },
            // renderRainbow: function(){
            //     //render all the colours - used to generate the thumbnail image
            //     var canvas = document.createElement('canvas');
            //     var ctx = canvas.getContext("2d");
            //     canvas.width = 870;
            //     canvas.height = 400;
            //     $("#demo").append(canvas);
            //     for (var i = 0 ; i < canvas.width ; i++){
            //         ctx.fillStyle = this.rgbToHex(this.nmToRGB(this.map(i, 0, canvas.width, this.ui.wavelength.range[0], this.ui.wavelength.range[1])));
            //         ctx.fillRect(i, 0, 1, canvas.height);
            //         ctx.fill();
            //     }
            // },
            // map: function(value, minFrom, maxFrom, minTo, maxTo){
            //     //http://stackoverflow.com/questions/4154969/how-to-map-numbers-in-range-099-to-range-1-01-0
            //     return minTo + (maxTo - minTo) * ((value - minFrom) / (maxFrom - minFrom));
            // }
        });
	*/

}
