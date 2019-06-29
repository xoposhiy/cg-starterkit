using lib;

namespace bot
{
    public class StateReader : BaseStateReader<State, StateInit>
    {
        protected override State Read(StateInit init)
        {
            var testInput = ReadInt();
            return new State();
        }
    }
}