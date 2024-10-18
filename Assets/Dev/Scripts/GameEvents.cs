using AVerse.Models;
using System;

namespace AVerse
{
    public static class GameEvents
    {
        public static Action<Property> PropertySelectionChanged;
        public static Action TransitionCompleted;
    }

}