using System;
using System.Collections.Generic;
public class saveContent
{
    public List<wave> waves = new List<wave>();
    public guanQiaType Gtp;
    public saveContent(List<wave> inWaves, guanQiaType gtp)
    {
        waves = inWaves;
        Gtp = gtp;
    }
}