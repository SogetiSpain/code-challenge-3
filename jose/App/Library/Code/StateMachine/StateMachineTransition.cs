namespace Library.App.Program.Code.StateMachine
{
    using Library.App.Program.Code.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class StateMachineTransition
    {
        readonly ConstantsApp.ProcessState CurrentState;
        readonly ConstantsApp.Command Command;

        public StateMachineTransition(ConstantsApp.ProcessState currentState, ConstantsApp.Command command)
        {
            CurrentState = currentState;
            Command = command;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateMachineTransition other = obj as StateMachineTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }

    }
}
