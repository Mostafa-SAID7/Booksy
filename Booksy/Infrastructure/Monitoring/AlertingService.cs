using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Booksy.Infrastructure.Monitoring
{
    /// <summary>
    /// Service for generating and dispatching alerts based on monitoring events
    /// </summary>
    public interface IAlertingService
    {
        Task SendAlertAsync(string alertType, string message, string severity);
        Task CheckThresholdsAsync();
    }

    /// <summary>
    /// Implementation of alerting service
    /// </summary>
    public class AlertingService : IAlertingService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AlertingService> _logger;
        private readonly Dictionary<string, int> _eventCounters = new();
        private readonly Dictionary<string, DateTime> _eventTimestamps = new();

        public AlertingService(IConfiguration configuration, ILogger<AlertingService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Send alert through configured channels
        /// </summary>
        public async Task SendAlertAsync(string alertType, string message, string severity)
        {
            var alertsEnabled = _configuration.GetValue<bool>("Monitoring:Alerts:Enabled");
            if (!alertsEnabled)
                return;

            _logger.LogWarning("ALERT [{Severity}]: {AlertType} - {Message}", severity, alertType, message);

            var minimumSeverityForEmail = _configuration["Monitoring:Alerts:Channels:Email:MinimumSeverity"] ?? "High";
            if (ShouldSendAlert(severity, minimumSeverityForEmail))
            {
                await SendEmailAlertAsync(alertType, message, severity);
            }

            var webhookUrl = _configuration["Monitoring:Alerts:Channels:Webhook:Url"];
            if (!string.IsNullOrEmpty(webhookUrl))
            {
                await SendWebhookAlertAsync(webhookUrl, alertType, message, severity);
            }

            var slackWebhook = _configuration["Monitoring:Alerts:Channels:Slack:WebhookUrl"];
            if (!string.IsNullOrEmpty(slackWebhook))
            {
                await SendSlackAlertAsync(slackWebhook, alertType, message, severity);
            }

            var pagerDutyKey = _configuration["Monitoring:Alerts:Channels:PagerDuty:IntegrationKey"];
            if (!string.IsNullOrEmpty(pagerDutyKey) && severity == "Critical")
            {
                await SendPagerDutyAlertAsync(pagerDutyKey, alertType, message);
            }

            var appInsightsKey = _configuration["Monitoring:Alerts:Channels:ApplicationInsights:InstrumentationKey"];
            if (!string.IsNullOrEmpty(appInsightsKey))
            {
                await SendApplicationInsightsAlertAsync(appInsightsKey, alertType, message, severity);
            }
        }

        /// <summary>
        /// Check thresholds and generate alerts if exceeded
        /// </summary>
        public async Task CheckThresholdsAsync()
        {
            // This would be called by a background service on a timer
            // It would check current metrics against configured thresholds
            await Task.CompletedTask;
        }

        /// <summary>
        /// Track event count for threshold checking
        /// </summary>
        public void TrackEvent(string eventKey)
        {
            if (!_eventCounters.ContainsKey(eventKey))
            {
                _eventCounters[eventKey] = 0;
                _eventTimestamps[eventKey] = DateTime.UtcNow;
            }

            _eventCounters[eventKey]++;
        }

        /// <summary>
        /// Send email alert
        /// </summary>
        private async Task SendEmailAlertAsync(string alertType, string message, string severity)
        {
            var enabled = _configuration.GetValue<bool>("Monitoring:Alerts:Channels:Email:Enabled");
            if (!enabled)
                return;

            var recipients = _configuration.GetSection("Monitoring:Alerts:Channels:Email:Recipients").Get<string[]>();
            if (recipients == null || recipients.Length == 0)
                return;

            _logger.LogInformation("Sending email alert to {Recipients}: {Message}", 
                string.Join(", ", recipients), message);

            // In production, integrate with email service
            // emailService.SendAsync(recipients, $"Alert: {alertType}", message);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Send webhook alert
        /// </summary>
        private async Task SendWebhookAlertAsync(string webhookUrl, string alertType, string message, string severity)
        {
            try
            {
                _logger.LogInformation("Sending webhook alert to {Url}", webhookUrl);

                // In production, call webhook with alert payload
                // using (var client = new HttpClient())
                // {
                //     var payload = new { alertType, message, severity, timestamp = DateTime.UtcNow };
                //     var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                //     await client.PostAsync(webhookUrl, content);
                // }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending webhook alert");
            }
        }

        /// <summary>
        /// Send Slack alert
        /// </summary>
        private async Task SendSlackAlertAsync(string webhookUrl, string alertType, string message, string severity)
        {
            try
            {
                _logger.LogInformation("Sending Slack alert");

                // In production, send to Slack webhook
                // Color based on severity
                var color = severity switch
                {
                    "Critical" => "danger",
                    "High" => "warning",
                    _ => "good"
                };

                // Slack message format
                // var payload = new {
                //     attachments = new[] {
                //         new {
                //             color,
                //             title = $"[{severity}] {alertType}",
                //             text = message,
                //             ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                //         }
                //     }
                // };

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending Slack alert");
            }
        }

        /// <summary>
        /// Send PagerDuty alert for critical incidents
        /// </summary>
        private async Task SendPagerDutyAlertAsync(string integrationKey, string alertType, string message)
        {
            try
            {
                _logger.LogInformation("Sending PagerDuty alert");

                // In production, call PagerDuty Events API
                // var payload = new {
                //     routing_key = integrationKey,
                //     event_action = "trigger",
                //     dedup_key = $"{alertType}-{DateTime.UtcNow:yyyyMMddHHmm}",
                //     payload = new {
                //         summary = message,
                //         severity = "critical",
                //         source = "Booksy API",
                //         component = "Security"
                //     }
                // };

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending PagerDuty alert");
            }
        }

        /// <summary>
        /// Send Application Insights event
        /// </summary>
        private async Task SendApplicationInsightsAlertAsync(string instrumentationKey, string alertType, string message, string severity)
        {
            try
            {
                _logger.LogInformation("Logging to Application Insights");

                // In production, use TelemetryClient
                // var telemetryClient = new TelemetryClient(new TelemetryConfiguration(instrumentationKey));
                // telemetryClient.TrackEvent($"Alert_{alertType}", new Dictionary<string, string>
                // {
                //     { "Message", message },
                //     { "Severity", severity }
                // });

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending Application Insights alert");
            }
        }

        /// <summary>
        /// Determine if alert should be sent based on severity
        /// </summary>
        private bool ShouldSendAlert(string alertSeverity, string minimumSeverity)
        {
            var severityLevels = new[] { "Low", "Medium", "High", "Critical" };
            var alertLevel = Array.IndexOf(severityLevels, alertSeverity);
            var minLevel = Array.IndexOf(severityLevels, minimumSeverity);

            return alertLevel >= minLevel;
        }
    }
}
