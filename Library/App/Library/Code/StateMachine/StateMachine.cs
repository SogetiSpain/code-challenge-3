namespace Library.App.Program.Code.StateMachine
{
    using Library.App.Program.Code.Utils;
    using Castle.DynamicProxy;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Infrastructure.Log.Installer;
    using Library.App.Program.Implementation;
    using Library.App.Program.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StateMachine
    {

        private StateMachineProcess _smProcess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StateMachine() 
        {
            _smProcess = new StateMachineProcess();
            _smProcess.CurrentState = ConstantsApp.ProcessState.StartState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandToExecute"></param>
        public void Transition(ConstantsApp.Command commandToExecute) 
        {
            try
            {
                _smProcess.MoveNext(commandToExecute);
            }
            catch (Exception ex) 
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>       

        public ConstantsApp.ProcessState GetCurrentState()
        {
            return _smProcess.CurrentState;
        }

        public bool IsFinish()
        {
            return (_smProcess.CurrentState == ConstantsApp.ProcessState.ExitState);
        }

    }


}
