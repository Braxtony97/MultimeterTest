namespace Base
{
    public static class SignalsProvider
    {
        public class ModeChangeSignal 
        {
            public MultimeterMode CurrentMode;

            public ModeChangeSignal(MultimeterMode mode) =>
                CurrentMode = mode;
        }

        public class DisplayChangeSignal 
        {
            public float Value;

            public DisplayChangeSignal(float value) => 
                Value = value;
        }

    }
}

