/* Booksy — Injected navigation bar for Swagger UI */
(function injectBooksyNav() {
  var NAV_ID = 'bk-swagger-nav-root';

  function build() {
    if (document.getElementById(NAV_ID)) return;

    var nav = document.createElement('nav');
    nav.id = NAV_ID;
    nav.className = 'bk-swagger-nav';
    nav.innerHTML =
      '<div class="bk-swagger-nav-inner">' +
        '<a href="/" class="bk-swagger-logo">' +
          '<svg width="18" height="18" viewBox="0 0 20 20" fill="currentColor" xmlns="http://www.w3.org/2000/svg">' +
            '<rect x="3" y="2" width="11" height="16" rx="2" fill="#0078d4"/>' +
            '<rect x="5" y="5" width="7" height="1.5" rx="1" fill="white"/>' +
            '<rect x="5" y="8.5" width="7" height="1.5" rx="1" fill="white"/>' +
            '<rect x="5" y="12" width="5" height="1.5" rx="1" fill="white"/>' +
          '</svg>' +
          '<span>Booksy</span>' +
          '<span class="bk-swagger-nav-badge">API</span>' +
        '</a>' +
        '<ul class="bk-swagger-links">' +
          '<li><a href="/">Home</a></li>' +
          '<li><a href="/docs.html">Docs</a></li>' +
          '<li><a href="/dashboard.html">Dashboard</a></li>' +
          '<li><a href="/stats.html">Stats</a></li>' +
          '<li><a href="/swagger" class="bk-active">Swagger</a></li>' +
        '</ul>' +
        '<div class="bk-swagger-cta">' +
          '<a href="/docs.html">\u2190 Back to Docs</a>' +
        '</div>' +
      '</div>';

    /* Insert as very first child of body */
    if (document.body.firstChild) {
      document.body.insertBefore(nav, document.body.firstChild);
    } else {
      document.body.appendChild(nav);
    }

    /* Hide the native Swagger topbar once React renders it */
    function hideSwaggerTopbar() {
      var topbar = document.querySelector('.swagger-ui .topbar');
      if (topbar) {
        topbar.style.setProperty('display', 'none', 'important');
        return true;
      }
      return false;
    }

    if (!hideSwaggerTopbar()) {
      /* React hasn't rendered yet — watch for it */
      var observer = new MutationObserver(function() {
        if (hideSwaggerTopbar()) {
          observer.disconnect();
        }
      });
      observer.observe(document.body, { childList: true, subtree: true });
    }
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', build);
  } else {
    build();
  }
})();
