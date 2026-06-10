using System.Text.Json;
using MQTTnet;
using MQTTnet.Protocol;

namespace SnapGuard.MockStation;

public static class MqttExtensions
{
    public static Task<MqttClientPublishResult> PublishAsync<T>(
        this IMqttClient client,
        string topic,
        T obj,
        MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.ExactlyOnce,
        JsonSerializerOptions? jsonSerializerOptions = null,
        CancellationToken cancellationToken = default
    )
    {
        string payload = JsonSerializer.Serialize(obj, jsonSerializerOptions);
        return client.PublishStringAsync(topic, payload, qos, false, cancellationToken);
    }
}
