using System;

namespace AsyncStreamsNetwork
{
    internal class ProgressStatus : IProgress<int>
    {
        readonly Action<int> action;

        public ProgressStatus(Action<int> progressAction) => action = progressAction;

        public void Report(int value) => action(value);
    }
}