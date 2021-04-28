using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
using HotChocolate.Execution.Channels;
using System;

namespace HotChocolate.Execution.Processing
{
    /// <inheritdoc/>
    internal class TaskBacklog : ITaskBacklog
    {
        private readonly ObjectPool<ResolverTask> _resolverTaskPool;
        private readonly ITaskStatistics _stats;
        private UnsortedChannel<IExecutionTask> _channel =
            new UnsortedChannel<IExecutionTask>(true);

        internal TaskBacklog(
            ITaskStatistics stats,
            ObjectPool<ResolverTask> resolverTaskPool)
        {
            _resolverTaskPool = resolverTaskPool;
            _stats = stats;
        }

        /// <inheritdoc/>
        public bool IsIdle => _channel.IsIdle;

        public Task WaitTillIdle(CancellationToken? ctx = null)
        {
            return _channel.WaitTillIdle(ctx);
        }

    /// <inheritdoc/>
    public bool TryTake(Action<IExecutionTask> receiver) =>
            _channel.TryRead(receiver);

        /// <inheritdoc/>
        public ValueTask<bool> WaitForTaskAsync(CancellationToken cancellationToken) =>
            _channel.WaitToReadAsync(cancellationToken);

        /// <inheritdoc/>
        public void Register(ResolverTaskDefinition taskDefinition)
        {
            ResolverTask resolverTask = _resolverTaskPool.Get();

            resolverTask.Initialize(
                taskDefinition.OperationContext,
                taskDefinition.Selection,
                taskDefinition.ResultMap,
                taskDefinition.ResponseIndex,
                taskDefinition.Parent,
                taskDefinition.Path,
                taskDefinition.ScopedContextData);

            Register(resolverTask);
        }

        public void Register(IExecutionTask task)
        {
            _stats.TaskCreated();
            _channel.TryWrite(task);
        }

        public void Complete() => _channel.Complete();

        public void Clear()
        {
            _channel = new UnsortedChannel<IExecutionTask>(true);
        }
    }
}
