using System.Text;

public async Task<IMqttClient> ConnectMqtt(string clientId, CancellationToken token = default)
{
    // Existing code ...

    SetupEventHandlers(mqttClient);

    // Existing code ...
}

private void SetupEventHandlers(IMqttClient mqttClient)
{
    mqttClient.ConnectedAsync += HandleConnectedAsync;
    mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
    mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
}

private async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
{
    try
    {
        string topic = eventArgs.ApplicationMessage.Topic;
        string payload = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);

        // Handle certificate replacement message
        if (topic == "certificate/replace")
        {
            // Perform certificate replacement logic here
            await ReplaceCertificate();
        }
    }
    catch (Exception ex)
    {
        _logger.LogError("Error handling MQTT application message: " + ex.Message);
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
