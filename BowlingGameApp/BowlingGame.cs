namespace BowlingGameApp
{
    public class BowlingGame
    {
        private readonly List<Frame> _frames = new List<Frame>();

        public int CurrentFrame => _frames.Count;

        public BowlingGame()
        {
            _frames.Add(new Frame(lastFrame: false));
        }

        public bool IsGameComplete()
        {
            return _frames.Count == 10 && _frames.Last().IsCompleted();
        }

        public int GetScore()
        {
            return GetFrameScores().Sum();
        }

        public int[] GetFrameScores()
        {
            int[] frameScored = new int[_frames.Count];
            for (int i = 0; i < frameScored.Length; i++)
            {
                frameScored[i] = _frames[i].GetScore(_frames.Skip(i + 1));
            }

            return frameScored;
        }

        public void PlayNext()
        {
            Frame currentFrame = _frames.Last();
            if (currentFrame.IsCompleted())
            {
                if (currentFrame.IsSpare() || currentFrame.IsStrike())
                {
                    Console.WriteLine($"Frame {CurrentFrame} finished. Player got {(currentFrame.IsSpare() ? "spare" : "strike")}");
                }

                currentFrame = new Frame(lastFrame: _frames.Count >= 9);
                _frames.Add(currentFrame);
            }

            currentFrame.Roll();
        }
    }
}
