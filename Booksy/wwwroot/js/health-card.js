/* Booksy Dashboard — health card countdown + status check */
(function healthCard() {
  var countdown  = 30;
  var lastStatus = null;   /* track previous status for change alerts */
  var cdEl       = document.getElementById('health-countdown');

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
        var status = r.ok ? 'healthy' : 'degraded';

        if (badge) {
          badge.className = status === 'healthy' ? 'health-badge healthy' : 'health-badge degraded';
          badge.innerHTML = status === 'healthy'
            ? '<span class="live-dot"></span> Healthy'
            : '<span class="live-dot" style="background:var(--warning)"></span> Degraded';
        }

        /* Toast only when status CHANGES */
        if (lastStatus !== null && lastStatus !== status) {
          if (status === 'degraded') {
            window.BooksyApp && window.BooksyApp.toast('⚠ API health degraded', 'warning', 6000);
          } else if (status === 'healthy') {
            window.BooksyApp && window.BooksyApp.toast('✓ API health restored', 'success', 4000);
          }
        }
        lastStatus = status;

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
        return r;
      })
      .catch(function() {
        if (badge) {
          badge.className = 'health-badge unhealthy';
          badge.innerHTML = '<span class="live-dot" style="background:var(--danger)"></span> Unhealthy';
        }
        if (lastStatus !== 'unhealthy') {
          window.BooksyApp && window.BooksyApp.toast('✗ API is unreachable', 'error', 8000);
        }
        lastStatus = 'unhealthy';
      });
  }

  /* Run immediately */
  checkHealth();
})();
