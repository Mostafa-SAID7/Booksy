## Monitoring Architecture

### Core Components

1. **MonitoringService** - Event tracking (auth failures, rate limits, exceptions, performance)
2. **PerformanceMonitoringMiddleware** - HTTP layer metrics (request/response time, slow endpoints)
3. **MonitoringBehavior** - CQRS layer tracking (command/query execution, exceptions)
4. **AlertingService** - Multi-channel delivery (Email, Slack, PagerDuty, Webhooks, App Insights)

---

## Alert Types & Triggers

### High Priority Alerts

| Alert | Trigger | Channel | Action |
|-------|---------|---------|--------|
| Failed Auth | 5+ in 5 min | Email | Review login attempts |
| Unauthorized Access | 10+ in 5 min | Email, Slack | Investigate user |
| Rate Limit Spike | >150 req/min | Webhook | Monitor traffic |
| Critical Exception | Any | Email | Debug & fix |
| Suspicious Activity | Detected | PagerDuty | Immediate action |

### Medium Priority Alerts

| Alert | Trigger | Channel |
|-------|---------|---------|
| Slow Endpoint | >5 sec response | App Insights |
| High Error Rate | >1% errors | Email |
| Database Slow Query | >1 sec | Logs |

### Low Priority Alerts

| Alert | Trigger | Channel |
|-------|---------|---------|
| Slow Query | >500ms | App Insights |
| Disk Space Low | >90% used | Email |

---

## Configuration

### Enable Monitoring
**File**: `appsettings.Monitoring.json`

```json
{
  "Monitoring": {
    "Enabled": true,
    "Alerts": {
      "Enabled": true,
      "Thresholds": {
        "FailedAuthenticationAttempts": {
          "Count": 5,
          "TimeWindowMinutes": 5,
          "Severity": "High"
        },
        "SlowEndpoints": {
          "ThresholdMs": 5000,
          "Severity": "Medium"
        }
      }
    }
  }
}
```

---

## Alert Channels

### 1. Email Alerts

**Setup**:
```json
{
  "Channels": {
    "Email": {
      "Enabled": true,
      "Recipients": ["admin@booksy.com", "security@booksy.com"],
      "MinimumSeverity": "High"
    }
  }
}
```

**Features**:
- SMTP already configured via EmailSettings
- Supports multiple recipients
- Severity-based filtering
- HTML formatted messages

### 2. Slack Alerts

**Setup**:
1. Create Slack app: https://api.slack.com/apps
2. Enable Incoming Webhooks
3. Create webhook for your channel
4. Copy webhook URL

```json
{
  "Channels": {
    "Slack": {
      "Enabled": true,
      "WebhookUrl": "https://hooks.slack.com/services/YOUR/WEBHOOK/URL",
      "Channel": "#security-alerts",
      "MinimumSeverity": "High"
    }
  }
}
```

**Features**:
- Color-coded by severity (red=critical, yellow=warning, green=good)
- Thread-based for related alerts
- Rich formatting with metadata

### 3. PagerDuty Alerts (Critical Only)

**Setup**:
1. Create PagerDuty service
2. Add Events API v2 integration
3. Copy integration key

```json
{
  "Channels": {
    "PagerDuty": {
      "Enabled": true,
      "IntegrationKey": "YOUR_INTEGRATION_KEY",
      "MinimumSeverity": "Critical"
    }
  }
}
```

**Features**:
- Incident creation
- Escalation policies
- On-call routing
- Alert deduplication

### 4. Webhook Alerts

**Setup**:
```json
{
  "Channels": {
    "Webhook": {
      "Enabled": true,
      "Url": "https://your-monitoring-service.com/api/alerts",
      "MinimumSeverity": "High"
    }
  }
}
```

**Payload Format**:
```json
{
  "alertType": "UnauthorizedAccess",
  "message": "10+ unauthorized access attempts detected",
  "severity": "High",
  "timestamp": "2026-07-09T12:00:00Z",
  "details": {
    "userId": "user-123",
    "ipAddress": "192.168.1.1",
    "count": 12,
    "timeWindow": "5 minutes"
  }
}
```

### 5. Application Insights

**Setup**:
1. Create Application Insights in Azure
2. Copy instrumentation key

```json
{
  "Channels": {
    "ApplicationInsights": {
      "Enabled": true,
      "InstrumentationKey": "YOUR_KEY",
      "MinimumSeverity": "Low"
    }
  }
}
```

**Features**:
- Performance analytics
- Custom event tracking
- Failure analysis
- Alerts & metrics explorer

---

## Viewing Metrics

### Local Logs
```bash
docker logs booksy-api | grep "ALERT"      # All alerts
docker logs booksy-api | grep "SECURITY"   # Security events
docker logs booksy-api | grep "PERFORMANCE"  # Performance warnings
```

### Application Insights
Access Azure Portal → Application Insights → Booksy API
- Performance → Response times
- Failures → Exception analysis
- Custom Events → Alert tracking

### Database Audit Logs
```sql
SELECT TOP 100 * FROM AuditLogs 
WHERE Timestamp > DATEADD(hour, -1, GETUTCDATE())
ORDER BY Timestamp DESC
```

---

## Common Issues & Solutions

### Alerts Not Sending
**Check 1**: Monitoring enabled in config  
**Check 2**: Channel enabled (Email, Slack, etc.)  
**Check 3**: Credentials valid (SMTP, webhook, integration key)  
**Check 4**: Review logs for error messages

### Too Many Alerts
**Solution 1**: Increase thresholds in `appsettings.Monitoring.json`  
**Solution 2**: Increase minimum severity (High/Critical only)  
**Solution 3**: Disable low-priority channels  

### Missing Metrics
**Check 1**: MonitoringBehavior registered in `CqrsExtensions.cs`  
**Check 2**: PerformanceMonitoringMiddleware added in `Program.cs`  
**Check 3**: Monitoring enabled in config

---

## Best Practices

1. **Severity Filtering**
   - Email: High & Critical only (reduce noise)
   - Slack: Medium & Critical (team awareness)
   - PagerDuty: Critical only (on-call focus)

2. **Alert Thresholds**
   - Development: Lenient (catch issues early)
   - Production: Strict (reduce false positives)

3. **Response Procedures**
   - Critical: < 5 minutes response time
   - High: < 15 minutes response time
   - Medium: < 1 hour review

4. **Alert Fatigue Prevention**
   - Deduplicate related alerts
   - Set appropriate thresholds
   - Regular threshold reviews
   - Clear alerting guidelines

---

## Key Metrics Dashboard

### Create Custom Dashboard

**Performance Metrics**:
- Average response time
- P95 response time
- Error rate
- Requests per minute

**Security Metrics**:
- Failed auth attempts
- Rate limit triggers
- Authorization failures
- Suspicious activity count

**System Metrics**:
- Memory usage
- Database connection pool
- Cache hit rate
- Disk space available

---

## Production Monitoring Setup

| Timeline | Tasks |
|----------|-------|
| **Day 1** | Email alerts enabled, Slack channel created, thresholds tuned |
| **Week 1** | PagerDuty integration, custom dashboards, on-call rotation |
| **Ongoing** | Monthly threshold review, alert fatigue reduction |

---

## Example Alert Responses

**Failed Authentication**: Check login attempt origin, verify user account, consider temporary IP block if suspicious.

**Rate Limiting Spike**: Check for legitimate bulk operations or bot activity, adjust limits if business need, block if malicious.

**Critical Exception**: Check service status, review connection pool settings, monitor concurrent load.

---

**Last Updated**: July 9, 2026  
**Status**: Production Ready ✅
