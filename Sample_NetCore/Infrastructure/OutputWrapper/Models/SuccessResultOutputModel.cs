using System;
using Newtonsoft.Json;

namespace Sample_NetCore.Infrastructure.OutputWrapper.Models
{
    /// <summary>
    /// 執行完成的 OutputModel
    /// </summary>
    /// <typeparam name="T">任意型別</typeparam>
    public class SuccessResultOutputModel<T>
    {
        /// <summary>
        /// The CorrelationId.
        /// </summary>
        [JsonProperty(PropertyName = "id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        /// <summary>
        /// The Api Version.
        /// </summary>
        [JsonProperty(PropertyName = "apiVersion", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ApiVersion { get; set; }

        /// <summary>
        /// The Method.
        /// </summary>
        [JsonProperty(PropertyName = "method", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }

        /// <summary>
        /// The Status.
        /// </summary>
        [JsonProperty(PropertyName = "status", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        /// Data.
        /// </summary>
        [JsonProperty(PropertyName = "data", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }
}