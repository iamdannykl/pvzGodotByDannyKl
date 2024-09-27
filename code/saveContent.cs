using System;
using System.Collections.Generic;
public class saveContent
{
    public List<wave> waves = new List<wave>();
    public guanQiaType Gtp;
    public string nameOfgq;
    public saveContent(List<wave> inWaves, guanQiaType gtp, string name)
    {
        waves = inWaves;
        Gtp = gtp;
        nameOfgq = name;
    }
    public saveContent()
    {

    }
}