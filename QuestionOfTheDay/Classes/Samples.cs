﻿using static System.Globalization.DateTimeFormatInfo;

namespace QuestionOfTheDay.Classes;
internal class Samples
{
    public static string[] MonthNames => CurrentInfo!.MonthNames[..^1];
    public static string[] DayNames => CurrentInfo!.DayNames;
}
