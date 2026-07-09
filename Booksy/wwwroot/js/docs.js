/* =====================================================
   Booksy Docs Page — Endpoint Search & Filter
   =====================================================
   Provides real-time search filtering for API endpoints
   and accordion toggle functionality
*/

(function initDocsPage() {
  // Search filter
  const searchInput = document.getElementById('search');
  if (!searchInput) return;

  searchInput.addEventListener('input', () => {
    const q = searchInput.value.toLowerCase().trim();

    // Filter individual endpoints
    document.querySelectorAll('.endpoint-item').forEach(item => {
      const text = item.textContent.toLowerCase();
      item.style.display = (!q || text.includes(q)) ? '' : 'none';
    });

    // Filter endpoint groups based on visible items
    document.querySelectorAll('.endpoint-group').forEach(group => {
      const visible = [...group.querySelectorAll('.endpoint-item')]
        .some(i => i.style.display !== 'none');
      group.style.display = visible ? '' : 'none';
      
      // Auto-expand groups when searching
      if (q && visible) {
        group.classList.add('open');
      }
    });

    // Reset visibility when query cleared
    if (!q) {
      document.querySelectorAll('.endpoint-group').forEach(g => {
        g.style.display = '';
      });
    }
  });

  // Endpoint group accordion toggle
  document.querySelectorAll('.endpoint-group-header').forEach(header => {
    header.addEventListener('click', () => {
      const group = header.closest('.endpoint-group');
      group.classList.toggle('open');
    });
  });
})();

