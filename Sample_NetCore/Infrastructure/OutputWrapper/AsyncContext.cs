using System;
using System.Threading;

namespace Sample_NetCore.Infrastructure.OutputWrapper
{
    /// <summary>
    /// class EvertrustAsyncContext.
    /// </summary>
    public static class AsyncContext
    {
        private static readonly AsyncLocal<Guid> _correlationId = new AsyncLocal<Guid>();
        private static readonly AsyncLocal<string> _domain = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> _version = new AsyncLocal<string>();

        /// <summary>
        /// CorrelationId
        /// </summary>
        public static Guid CorrelationId
        {
            get => _correlationId.Value;
            set => _correlationId.Value = value;
        }

        /// <summary>
        /// Domain
        /// </summary>
        public static string Domain
        {
            get => _domain.Value;
            set => _domain.Value = value;
        }

        /// <summary>
        /// Version
        /// </summary>
        public static string Version
        {
            get => _version.Value;
            set => _version.Value = value;
        }
    }
}