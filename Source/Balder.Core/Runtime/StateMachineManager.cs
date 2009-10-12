using System.Collections.Generic;

namespace Balder.Core.Runtime
{
	public class StateMachineManager
	{
		public static readonly StateMachineManager Instance = new StateMachineManager();

		private List<IStateMachine> _stateMachines;

		private StateMachineManager()
		{
			_stateMachines = new List<IStateMachine>();
		}

		public void Register(IStateMachine stateMachine)
		{
			_stateMachines.Add(stateMachine);
		}

		public void Unreqister(IStateMachine stateMachine)
		{
			_stateMachines.Remove(stateMachine);
		}


		internal void Run()
		{
			foreach (IStateMachine stateMachine in _stateMachines)
			{
				stateMachine.Execute();
			}
		}
	}
}
