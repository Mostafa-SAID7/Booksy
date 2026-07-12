/* =====================================================
   Booksy — Shared Application JS
   =====================================================
   Global utilities and helpers used across all pages
   - API helpers
   - Formatters
   - Navigation
   - Skeleton loaders
   - Toast notifications
   - Counter animations
   - HTML escaping
*/

/* ----- Navigation ----- */
(function initNav() {
  const path = window.location.pathname.replace(/\/$/, '') || '/';
  document.querySelectorAll('.nav-links a').forEach(a => {
    const href = a.getAttribute('href').replace(/\/$/, '') || '/';
    if (path === href || (href !== '/' && path.startsWith(href))) {
      a.classList.add('active');
    }
  });

  const toggle = document.getElementById('nav-toggle');
  const links  = document.getElementById('nav-links');
  if (toggle && links) {
    toggle.addEventListener('click', () => links.classList.toggle('open'));
    document.addEventListener('click', e => {
      if (!toggle.contains(e.target) && !links.contains(e.target)) {
        links.classList.remove('open');
      }
    });
  }
})();

/* ----- API helpers ----- */
const API = {
  base: '',
  async get(path) {
    const res = await fetch(this.base + path, {
      headers: { 'Accept': 'application/json' }
    });
    if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
    return res.json();
  },
  async post(path, data) {
    const res = await fetch(this.base + path, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
    return res.json();
  }
};

/* ----- Formatters ----- */
const fmt = {
  number(n) {
    if (n == null) return '—';
    if (n >= 1_000_000) return (n / 1_000_000).toFixed(1) + 'M';
    if (n >= 1_000)     return (n / 1_000).toFixed(1) + 'K';
    return Number(n).toLocaleString();
  },

  currency(n) {
    if (n == null) return '—';
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      maximumFractionDigits: 0
    }).format(n);
  },

  rating(n) {
    if (!n) return '—';
    const stars = Math.round(n);
    return '★'.repeat(stars) + '☆'.repeat(5 - stars) + ` (${n.toFixed(1)})`;
  },

  percent(n) {
    if (n == null) return '—';
    return n.toFixed(1) + '%';
  }
};

/* ----- Skeleton loader ----- */
function skeletonStat() {
  return `<div class="stat-card">
    <div class="skeleton skeleton-text" style="width:60%;height:12px"></div>
    <div class="skeleton skeleton-value"></div>
    <div class="skeleton skeleton-text" style="width:40%;height:10px"></div>
  </div>`;
}

/* ----- Toast notifications ----- */
function toast(msg, type = 'info', duration = 4000) {
  let container = document.getElementById('toasts');
  if (!container) {
    container = document.createElement('div');
    container.id = 'toasts';
    container.className = 'toast-container';
    document.body.appendChild(container);
  }

  const icons = {
    success: 'bi-check-circle-fill',
    error: 'bi-x-circle-fill',
    info: 'bi-info-circle-fill'
  };

  const el = document.createElement('div');
  el.className = `toast ${type}`;
  el.innerHTML = `<i class="bi ${icons[type] || icons.info}"></i><span>${escapeHtml(msg)}</span>`;
  container.appendChild(el);

  setTimeout(() => el.remove(), duration);
}

/* ----- Animate counter ----- */
function animateCounter(el, target, formatter = fmt.number, duration = 800) {
  const start = performance.now();
  const num = parseFloat(target) || 0;

  const tick = (now) => {
    const progress = Math.min((now - start) / duration, 1);
    const ease = 1 - Math.pow(1 - progress, 3);
    el.textContent = formatter(num * ease);

    if (progress < 1) {
      requestAnimationFrame(tick);
    } else {
      el.textContent = formatter(num);
    }
  };

  requestAnimationFrame(tick);
}

/* ----- HTML escaping (use for all API-sourced values in innerHTML) ----- */
function escapeHtml(str) {
  if (str == null) return '';
  return String(str)
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
    .replace(/'/g, '&#39;');
}

/* ----- Expose globals ----- */
window.BooksyApp = {
  API,
  fmt,
  toast,
  animateCounter,
  skeletonStat,
  escapeHtml
};
