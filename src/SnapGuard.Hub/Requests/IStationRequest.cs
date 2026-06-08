using System.Text.Json.Serialization;
using MQTTnet.Protocol;

namespace SnapGuard.Hub.Requests;

public interface IStationRequest
{
    [JsonIgnore]
    string TopicName { get; }

    [JsonIgnore]
    MqttQualityOfServiceLevel QoSLevel { get; }

    [JsonIgnore]
    TimeSpan Timeout { get; }

    [JsonIgnore]
    int DeviceId { get; set; }

    int Id { get; set; }
}

public interface IStationRequest<TResult> : IStationRequest;
