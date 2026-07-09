/* =====================================================
   Booksy Stats Page — Live Data Loader
   All API-sourced strings are escaped via escapeHtml
   before being inserted into the DOM.
   =====================================================
*/

(function init() {
  const { API, fmt, toast, animateCounter, escapeHtml } = window.BooksyApp;

  async function loadDashboard() {
    try {
      const data = await API.get('/api/Statistics/dashboard');
      const d = data.data || data;

      const el = document.getElementById('dashboard-stats');
      el.innerHTML = `
        <div class="stat-card">
          <div class="stat-icon blue"><i class="bi bi-people-fill"></i></div>
          <div class="stat-label">Total Users</div>
          <div class="stat-value" data-target="${escapeHtml(d.totalUsers ?? 0)}" data-type="number">—</div>
          <div class="stat-sub">Registered accounts</div>
        </div>

        <div class="stat-card">
          <div class="stat-icon green"><i class="bi bi-journal-richtext"></i></div>
          <div class="stat-label">Total Books</div>
          <div class="stat-value" data-target="${escapeHtml(d.totalBooks ?? 0)}" data-type="number">—</div>
          <div class="stat-sub">Active in catalog</div>
        </div>

        <div class="stat-card">
          <div class="stat-icon yellow"><i class="bi bi-receipt"></i></div>
          <div class="stat-label">Total Orders</div>
          <div class="stat-value" data-target="${escapeHtml(d.totalOrders ?? 0)}" data-type="number">—</div>
          <div class="stat-sub">All time</div>
        </div>

        <div class="stat-card">
          <div class="stat-icon cyan"><i class="bi bi-currency-dollar"></i></div>
          <div class="stat-label">Total Revenue</div>
          <div class="stat-value" data-target="${escapeHtml(d.totalRevenue ?? 0)}" data-type="currency">—</div>
          <div class="stat-sub">Avg order: ${escapeHtml(fmt.currency(d.averageOrderValue))}</div>
        </div>
      `;

      el.querySelectorAll('.stat-value').forEach(v => {
        const n = parseFloat(v.dataset.target) || 0;
        const type = v.dataset.type;
        animateCounter(v, n, type === 'currency' ? fmt.currency : fmt.number);
      });
    } catch (e) {
      showError('Dashboard stats: ' + e.message);
    }
  }

  async function loadBookStats() {
    try {
      const data = await API.get('/api/Statistics/books');
      const d = data.data || data;

      const rows = [
        ['bi-journal-richtext', 'Total Books', fmt.number(d.totalBooks), ''],
        ['bi-person-lines-fill', 'Authors', fmt.number(d.totalAuthors), ''],
        ['bi-tags', 'Categories', fmt.number(d.totalCategories), ''],
        ['bi-exclamation-triangle', 'Out of Stock', fmt.number(d.outOfStockBooks), 'color:var(--danger)'],
        ['bi-exclamation-circle', 'Low Stock (≤10)', fmt.number(d.lowStockBooks), 'color:var(--warning)'],
        ['bi-tag', 'Avg Price', fmt.currency(d.averageBookPrice), ''],
        ['bi-star-half', 'Avg Rating', d.averageRating ? '★'.repeat(Math.round(d.averageRating)) + ' ' + escapeHtml(String(d.averageRating)) : '—', '']
      ];

      document.getElementById('book-stats-content').innerHTML = rows.map(([icon, label, val, style]) => 
        `<div class="metric-row">
          <span class="metric-label"><i class="bi ${escapeHtml(icon)}" style="${escapeHtml(style)}"></i> ${escapeHtml(label)}</span>
          <span class="metric-val" style="${escapeHtml(style)}">${val}</span>
        </div>`
      ).join('');
    } catch (e) {
      document.getElementById('book-stats-content').innerHTML = 
        `<div class="error-state"><i class="bi bi-exclamation-triangle-fill"></i>${escapeHtml(e.message)}</div>`;
    }
  }

  async function loadUserStats() {
    try {
      const data = await API.get('/api/Statistics/users');
      const d = data.data || data;

      const rows = [
        ['bi-person-check', 'Active Users', fmt.number(d.totalActiveUsers), 'color:var(--success)'],
        ['bi-person-x', 'Inactive Users', fmt.number(d.totalInactiveUsers), ''],
        ['bi-person-plus', 'New This Month', fmt.number(d.newUsersThisMonth), 'color:var(--accent)'],
        ['bi-cart-check', 'Users with Orders', fmt.number(d.usersWithOrders), ''],
        ['bi-currency-dollar', 'Avg Order Value', fmt.currency(d.averageUserOrderValue), '']
      ];

      document.getElementById('user-stats-content').innerHTML = rows.map(([icon, label, val, style]) =>
        `<div class="metric-row">
          <span class="metric-label"><i class="bi ${escapeHtml(icon)}" style="${escapeHtml(style)}"></i> ${escapeHtml(label)}</span>
          <span class="metric-val" style="${escapeHtml(style)}">${val}</span>
        </div>`
      ).join('');
    } catch (e) {
      document.getElementById('user-stats-content').innerHTML =
        `<div class="error-state"><i class="bi bi-exclamation-triangle-fill"></i>${escapeHtml(e.message)}</div>`;
    }
  }

  async function loadTopBooks() {
    try {
      const data = await API.get('/api/Reports/top-books?limit=5');
      const books = data.data || data;

      const el = document.getElementById('top-books-content');
      if (!books || !books.length) {
        el.innerHTML = '<div class="empty-state"><i class="bi bi-journal-x"></i><p>No sales data yet</p></div>';
        return;
      }

      const medals = ['🥇', '🥈', '🥉'];
      el.innerHTML = '<div class="top-books-list">' + books.map((b, i) => {
        const title = escapeHtml(b.title ?? b.bookTitle ?? 'Unknown');
        const author = escapeHtml(b.author ?? b.authorName ?? '');
        const sold = escapeHtml(fmt.number(b.totalSold ?? b.quantitySold ?? b.soldCount ?? 0));
        const rank = i < 3 ? medals[i] : String(i + 1);

        return `<div class="book-rank-item">
          <div class="rank-num ${i < 3 ? 'top' : ''}">${escapeHtml(rank)}</div>
          <div class="book-info">
            <div class="book-title truncate">${title}</div>
            <div class="book-author">${author}</div>
          </div>
          <div class="book-sales">
            <div class="book-sales-num">${sold}</div>
            <div class="book-sales-label">sold</div>
          </div>
        </div>`;
      }).join('') + '</div>';
    } catch (e) {
      document.getElementById('top-books-content').innerHTML =
        `<div class="error-state"><i class="bi bi-exclamation-triangle-fill"></i>${escapeHtml(e.message)}</div>`;
    }
  }

  async function loadRevenue() {
    try {
      const data = await API.get('/api/Reports/monthly-revenue?months=6');
      const months = data.data || data;

      const el = document.getElementById('revenue-content');
      if (!months || !months.length) {
        el.innerHTML = '<div class="empty-state"><i class="bi bi-bar-chart"></i><p>No revenue data yet</p></div>';
        return;
      }

      const max = Math.max(...months.map(m => m.revenue ?? m.totalRevenue ?? 0));

      // Build bars safely without innerHTML for dynamic parts
      const container = document.createElement('div');
      container.className = 'revenue-chart';

      months.forEach(m => {
        const val = m.revenue ?? m.totalRevenue ?? 0;
        const pct = max > 0 ? (val / max) * 100 : 0;
        const label = String(m.month ?? m.monthName ?? '');
        const short = label.slice(0, 3);

        const wrap = document.createElement('div');
        wrap.className = 'revenue-bar-wrap';
        wrap.title = `${label}: ${fmt.currency(val)}`;

        const bar = document.createElement('div');
        bar.className = 'revenue-bar';
        bar.style.height = Math.max(pct, 3) + '%';

        const lbl = document.createElement('div');
        lbl.className = 'revenue-label';
        lbl.textContent = short; // textContent — no XSS

        wrap.appendChild(bar);
        wrap.appendChild(lbl);
        container.appendChild(wrap);
      });

      const footer = document.createElement('div');
      footer.className = 'flex justify-between mt-8';

      const lo = document.createElement('span');
      lo.className = 'text-xs text-subtle';
      lo.textContent = '$0';

      const hi = document.createElement('span');
      hi.className = 'text-xs text-subtle';
      hi.textContent = 'Max: ' + fmt.currency(max);

      footer.appendChild(lo);
      footer.appendChild(hi);

      el.innerHTML = '';
      el.appendChild(container);
      el.appendChild(footer);
    } catch (e) {
      document.getElementById('revenue-content').innerHTML =
        `<div class="error-state"><i class="bi bi-exclamation-triangle-fill"></i>${escapeHtml(e.message)}</div>`;
    }
  }

  function showError(msg) {
    const el = document.getElementById('error-banner');
    el.classList.remove('hidden');
    document.getElementById('error-msg').textContent = msg; // textContent — safe
  }

  function updateTimestamp() {
    const el = document.getElementById('last-updated');
    el.innerHTML = '<i class="bi bi-clock"></i> ';
    el.appendChild(document.createTextNode('Updated ' + new Date().toLocaleTimeString()));
  }

  async function loadAll() {
    document.getElementById('error-banner').classList.add('hidden');

    const btn = document.getElementById('refresh-btn');
    btn.disabled = true;
    btn.innerHTML = '<i class="bi bi-arrow-clockwise"></i> Loading…';

    await Promise.allSettled([
      loadDashboard(),
      loadBookStats(),
      loadUserStats(),
      loadTopBooks(),
      loadRevenue()
    ]);

    updateTimestamp();
    btn.disabled = false;
    btn.innerHTML = '<i class="bi bi-arrow-clockwise"></i> Refresh';
  }

  document.getElementById('refresh-btn').addEventListener('click', loadAll);
  loadAll();
})();
