public async Task SendCertificateReplacementMessage()
{
    try
    {
        IMqttClient client = await _mqttConnectionHelper.ConnectMqtt(_AtlantisServiceClientName);
        if (client.IsConnected)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("certificate/replace")
                .WithPayload("Certificate replacement required")
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await client.PublishAsync(message);
            _logger.LogInformation("Certificate replacement message published");
        }
        else
        {
            _logger.LogError("MQTT client is not connected.");
        }

        await client.DisconnectAsync();
    }
    catch (Exception ex)
    {
        _logger.LogError("Error publishing certificate replacement message: " + ex.Message);
        throw;
    }
}
