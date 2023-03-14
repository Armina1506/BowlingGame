namespace BowlingGameApp
{
    public class Frame
    {
        private readonly static Random _rnd = new Random();
        private readonly bool _lastframe = false;
        private readonly int[] _rolls = new int[2];

        private int _bonusRoll = 0;
        private int _rollIndex = 0;

        public Frame(bool lastFrame)
        {
            _lastframe = lastFrame;
        }

        public void Roll()
        {
            var isBonusRoll = _lastframe && _rollIndex == _rolls.Length;
            int pinsRemaining = 10 - _rolls.Sum();
            if (isBonusRoll)
            {
                // the final bonus roll gets 10 new pins
                pinsRemaining = 10;
            }

            // roll the ball. Randomly decide how many pins was knocked down.
            // change pins knocked down to 10 if you want strikes
            int pinsKnockedDown = _rnd.Next(0, pinsRemaining + 1);

            if (isBonusRoll)
            {
                _rollIndex++;
                _bonusRoll = pinsKnockedDown;
            }
            else
            {
                _rolls[_rollIndex++] = pinsKnockedDown;
            }
        }

        public bool IsStrike()
        {
            return _rolls[0] == 10;
        }

        public bool IsSpare()
        {
            return !IsStrike() && _rolls[0] + _rolls[1] == 10;
        }

        public bool IsCompleted()
        {
            if (!_lastframe)
            {
                return IsStrike() || _rollIndex >= 2;
            }

            // in case of last frame, we have three rolls if we rolled a strike or spare
            return !((IsStrike() || IsSpare()) && _rollIndex < 3);
        }

        public int GetScore(IEnumerable<Frame> futureFrames)
        {
            return _rolls.Sum() + FrameBonus(futureFrames);
        }

        private int FrameBonus(IEnumerable<Frame> futureFrames)
        {
            if (_lastframe && (IsSpare() || IsStrike()))
            {
                return _bonusRoll;
            }

            if (!futureFrames.Any())
            {
                return 0;
            }

            if (IsSpare())
            {
                return futureFrames.First()._rolls[0];
            }

            if (IsStrike())
            {
                if (futureFrames.First().IsStrike())
                {
                    if (futureFrames.First()._lastframe)
                    {
                        // if this is second last frame, count the two bonus rolls from that last frame, instead of two consecutive frames
                        return futureFrames.First()._rolls.Sum();
                    }

                    // we cannot count the bonus for this strike before the next two frames are finished 
                    if (futureFrames.Count() < 2)
                    {
                        return 0;
                    }

                    // count the bonus from the next two frames
                    return futureFrames.First()._rolls.Sum() + futureFrames.Skip(1).First()._rolls[0];
                }

                return futureFrames.First()._rolls.Sum();
            }

            return 0;
        }
    }
}
