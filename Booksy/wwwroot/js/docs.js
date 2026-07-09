/* =====================================================
   Booksy Docs Page — Endpoint Search & Filter
   =====================================================
   Provides real-time search filtering for API endpoints
*/

(function initSearch() {
  const searchInput = document.getElementById('search');
  if (!searchInput) return;

  searchInput.addEventListener('input', () => {
    const q = searchInput.value.toLowerCase().trim();

    // Filter individual endpoints
    document.querySelectorAll('.endpoint-item').forEach(item => {
      item.style.display = (!q || item.textContent.toLowerCase().includes(q)) ? '' : 'none';
    });

    // Filter endpoint groups based on visible items
    document.querySelectorAll('.endpoint-group').forEach(group => {
      const hasVisible = [...group.querySelectorAll('.endpoint-item')].some(i => i.style.display !== 'none');
      group.style.display = hasVisible ? '' : 'none';
      if (q && hasVisible) group.classList.add('open');
    });

    // Reset visibility when query cleared
    if (!q) {
      document.querySelectorAll('.endpoint-group').forEach(g => {
        g.style.display = '';
      });
    }
  });
})();
