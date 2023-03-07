public class MusicMath
{
    public static float BPS(int bpm)
    {
        return bpm / 60f;
    }

    public static float DurationPerBeat(int bpm)
    {
        return 1f / BPS(bpm);
    }
}