using System.Text.Json.Serialization;
using MQTTnet.Protocol;

namespace SnapGuard.Hub.Requests;

public abstract class StationRequestBase(
    string topicName,
    MqttQualityOfServiceLevel qosLevel = MqttQualityOfServiceLevel.AtLeastOnce,
    TimeSpan timeout = default
) : IStationRequest
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string TopicName { get; } = topicName;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public MqttQualityOfServiceLevel QoSLevel { get; } = qosLevel;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public TimeSpan Timeout { get; } = timeout;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public required int DeviceId { get; set; }

    public int Id { get; set; }
}

public abstract class StationRequestBase<TResult>(
    string topicName,
    MqttQualityOfServiceLevel qosLevel = MqttQualityOfServiceLevel.AtLeastOnce
) : StationRequestBase(topicName, qosLevel), IStationRequest<TResult>
{ }
