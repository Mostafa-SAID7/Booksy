/* Booksy Dashboard — health card countdown + status check */
(function healthCard() {
    var countdown = 30;
    var cdEl = document.getElementById('health-countdown');

    function tick() {
        if (!cdEl) return;
        countdown--;
        cdEl.textContent = countdown;
        if (countdown <= 0) {
            countdown = 30;
            checkHealth();
        }
    }
    setInterval(tick, 1000);

    function checkHealth() {
        var badge   = document.getElementById('health-status-badge');
        var content = document.getElementById('health-content');

        fetch('/api/Statistics/dashboard')
            .then(function(r) {
                if (badge) {
                    if (r.ok) {
                        badge.className = 'health-badge healthy';
                        badge.innerHTML = '<span class="live-dot"></span> Healthy';
                    } else {
                        badge.className = 'health-badge degraded';
                        badge.innerHTML = '<span class="live-dot" style="background:var(--warning)"></span> Degraded';
                    }
                }
                if (content && r.ok) {
                    content.innerHTML =
                        '<div class="health-service-item">' +
                            '<span class="health-dot ok"></span>' +
                            '<span>API Server</span>' +
                            '<span class="text-muted text-xs" style="margin-left:auto">Online</span>' +
                        '</div>' +
                        '<div class="health-service-item">' +
                            '<span class="health-dot ok"></span>' +
                            '<span>PostgreSQL</span>' +
                            '<span class="text-muted text-xs" style="margin-left:auto">Connected</span>' +
                        '</div>' +
                        '<div class="health-service-item">' +
                            '<span class="health-dot ok"></span>' +
                            '<span>Statistics API</span>' +
                            '<span class="text-muted text-xs" style="margin-left:auto">Responding</span>' +
                        '</div>';
                }
            })
            .catch(function() {
                if (badge) {
                    badge.className = 'health-badge unhealthy';
                    badge.innerHTML = '<span class="live-dot" style="background:var(--danger)"></span> Unhealthy';
                }
            });
    }

    checkHealth();
})();
