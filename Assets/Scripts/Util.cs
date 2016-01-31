using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Util {
    public static string NiceLongString(Int64 input) {
        if(input > 1000000000000000000L) {
            return ((double)(input) / 1000000000000000000.0).ToString("0.##") + "E";
        } else if(input > 1000000000000000L) {
            return ((double)(input) / 1000000000000000.0).ToString("0.##") + "P";
        } else if(input > 1000000000000L) {
            return ((double)(input) / 1000000000000.0).ToString("0.##") + "T";
        } else if(input > 1000000000L) {
            return ((double)(input) / 1000000000.0).ToString("0.##") + "G";
        } else if(input > 1000000L) {
            return ((double)(input) / 1000000.0).ToString("0.##") + "M";
        } else if(input > 1000L) {
            return ((double)(input) / 1000.0).ToString("0.##") + "K";
        } else {
            return input.ToString();
        }
    }
}