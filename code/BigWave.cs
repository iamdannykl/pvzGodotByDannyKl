using System;
using System.Collections.Generic;
public class BigWave
{
    public wave bigWave;
    public float location;
    public List<wave> miniWaves = new List<wave>();
    public BigWave(wave inWave)
    {
        bigWave = inWave;
    }
}