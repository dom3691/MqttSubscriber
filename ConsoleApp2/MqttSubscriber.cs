public async Task RegisterMessageReceivedHandler(CancellationToken cancellationToken)
{
    _mqttMessagingClient.MessageReceived += async (sender, eventArgs) =>
        await OnMqttMessageReceived(eventArgs);
}

public async Task Subscribe(CancellationToken cancellationToken)
{
    var topicFilters = new List<MqttTopicFilter>
    {
        // Existing topics
        new MqttTopicFilterBuilder()
            .WithTopic(string.Format(_configuration.DeviceCommandsTopic, _mqttClientOptions.ClientId))
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build(),

        new MqttTopicFilterBuilder()
            .WithTopic(string.Format(_configuration.DevicePolicyTopic, _mqttClientOptions.ClientId))
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build(),

        // Add topic filter for certificate replacement
        new MqttTopicFilterBuilder()
            .WithTopic($"reprovision/certificate/{_mqttClientOptions.ClientId}")
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build()
    };

    await _mqttMessagingClient.SubscribeAsync(topicFilters, cancellationToken);
}

private async Task OnMqttMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
{
    // Handle other message types

    // Handle certificate replacement message
    if (topic.StartsWith($"reprovision/certificate/{_mqttClientOptions.ClientId}"))
    {
        // Perform certificate replacement logic here
        await ReplaceCertificate();
    }
}

private async Task ReplaceCertificate()
{
    try
    {
        

        _logger.LogInformation("Certificate replacement logic executed successfully.");
    }
    catch (Exception ex)
    {
        _logger.LogError("Error replacing certificate: " + ex.Message);
    }
}
