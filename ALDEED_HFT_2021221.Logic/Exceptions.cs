using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    class PvpEventNotSurvivedException : Exception
    {
        public PvpEventNotSurvivedException()
        {

        }
        public PvpEventNotSurvivedException(string message) : base(message)
        {

        }
    }

    class InvalidLocationException : Exception
    {
        public InvalidLocationException()
        {

        }

        public InvalidLocationException(string message) : base(message)
        {

        }
    }

    class PlayerKilledException : Exception
    {
        public PlayerKilledException()
        {

        }

        public PlayerKilledException(string message) : base(message)
        {

        }
    }

    class NoSuchPlayerException : Exception
    {
        public NoSuchPlayerException()
        {

        }

        public NoSuchPlayerException(string message) : base(message)
        {

        }
    }
    class PlayerNotKilledException : Exception
    {
        public PlayerNotKilledException()
        {

        }

        public PlayerNotKilledException(string message) : base(message)
        {

        }
    }
}
