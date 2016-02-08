namespace Library.App.Program.Code.StateMachine
{
    using Library.App.Program.Code.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class StateMachineProcess
    {
        Dictionary<StateMachineTransition, ConstantsApp.ProcessState> _smTransitions;

        public ConstantsApp.ProcessState CurrentState { get; set; }

        public StateMachineProcess()
        {
            CurrentState = ConstantsApp.ProcessState.StartState;
            _smTransitions = new Dictionary<StateMachineTransition, ConstantsApp.ProcessState>
            {
                #region WrongTransitions
                { new StateMachineTransition(ConstantsApp.ProcessState.BookBookingState, ConstantsApp.Command.InvalidInput), ConstantsApp.ProcessState.ErrorState },
                { new StateMachineTransition(ConstantsApp.ProcessState.BookCreationState, ConstantsApp.Command.InvalidInput), ConstantsApp.ProcessState.ErrorState },
                { new StateMachineTransition(ConstantsApp.ProcessState.BookReturnState, ConstantsApp.Command.InvalidInput), ConstantsApp.ProcessState.ErrorState },
                { new StateMachineTransition(ConstantsApp.ProcessState.ErrorState, ConstantsApp.Command.InvalidInput), ConstantsApp.ProcessState.ErrorState },
                { new StateMachineTransition(ConstantsApp.ProcessState.ExitState, ConstantsApp.Command.InvalidInput), ConstantsApp.ProcessState.ErrorState },
                { new StateMachineTransition(ConstantsApp.ProcessState.StartState, ConstantsApp.Command.InvalidInput), ConstantsApp.ProcessState.ErrorState },
                #endregion WrongTransitions

                #region UserInputTransitions
                { new StateMachineTransition(ConstantsApp.ProcessState.StartState, ConstantsApp.Command.Exit), ConstantsApp.ProcessState.ExitState },
                { new StateMachineTransition(ConstantsApp.ProcessState.StartState, ConstantsApp.Command.BookCreation), ConstantsApp.ProcessState.BookCreationState },
                { new StateMachineTransition(ConstantsApp.ProcessState.StartState, ConstantsApp.Command.BookBooking), ConstantsApp.ProcessState.BookBookingState },
                { new StateMachineTransition(ConstantsApp.ProcessState.StartState, ConstantsApp.Command.BookReturn), ConstantsApp.ProcessState.BookReturnState },
                { new StateMachineTransition(ConstantsApp.ProcessState.StartState, ConstantsApp.Command.PenaltyPayment), ConstantsApp.ProcessState.PayPenlaty },

                #endregion UserInputTransitions

                #region BackToStartStateTransitions
                { new StateMachineTransition(ConstantsApp.ProcessState.BookBookingState, ConstantsApp.Command.Begin), ConstantsApp.ProcessState.StartState },
                { new StateMachineTransition(ConstantsApp.ProcessState.BookCreationState, ConstantsApp.Command.Begin), ConstantsApp.ProcessState.StartState },
                { new StateMachineTransition(ConstantsApp.ProcessState.BookReturnState, ConstantsApp.Command.Begin), ConstantsApp.ProcessState.StartState },
                { new StateMachineTransition(ConstantsApp.ProcessState.ErrorState, ConstantsApp.Command.Begin), ConstantsApp.ProcessState.StartState },                
                { new StateMachineTransition(ConstantsApp.ProcessState.PayPenlaty, ConstantsApp.Command.Begin), ConstantsApp.ProcessState.StartState },                
                #endregion BackToStartStateTransitions

            };
        }

        public ConstantsApp.ProcessState GetNext(ConstantsApp.Command command)
        {            
            StateMachineTransition transition = new StateMachineTransition(CurrentState, command);
            ConstantsApp.ProcessState nextState;
            _smTransitions.TryGetValue(transition, out nextState);
            return nextState;
        }

        public ConstantsApp.ProcessState MoveNext(ConstantsApp.Command command)
        {
            try
            {
                CurrentState = GetNext(command);
                return CurrentState;
            }
            catch (Exception ex) 
            {
                throw (ex);
            }
        }

    }
}
