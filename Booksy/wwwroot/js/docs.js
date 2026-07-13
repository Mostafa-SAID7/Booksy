/* =====================================================
   Booksy Docs Page — Endpoint Search, Filter & Copy
   =====================================================
*/

(function initDocsPage() {
  /* ── Copy to clipboard ── */
  function addCopyButtons() {
    document.querySelectorAll('.endpoint-item').forEach(function(item) {
      /* avoid double-adding */
      if (item.querySelector('.endpoint-copy-btn')) return;

      var pathEl = item.querySelector('.endpoint-path');
      if (!pathEl) return;

      var btn = document.createElement('button');
      btn.className = 'endpoint-copy-btn';
      btn.setAttribute('aria-label', 'Copy endpoint');
      btn.setAttribute('title', 'Copy endpoint path');
      btn.innerHTML = '<i class="bi bi-clipboard"></i>';

      btn.addEventListener('click', function(e) {
        e.stopPropagation();
        /* Get clean path text (strip lock icon text) */
        var raw = pathEl.cloneNode(true);
        raw.querySelectorAll('i').forEach(function(el) { el.remove(); });
        var text = raw.textContent.trim();

        if (navigator.clipboard && navigator.clipboard.writeText) {
          navigator.clipboard.writeText(text).then(function() {
            btn.innerHTML = '<i class="bi bi-check2" style="color:var(--success)"></i>';
            window.BooksyApp && window.BooksyApp.toast('Copied: ' + text, 'success', 2200);
            setTimeout(function() { btn.innerHTML = '<i class="bi bi-clipboard"></i>'; }, 2200);
          }).catch(function() {
            window.BooksyApp && window.BooksyApp.toast('Could not copy', 'error', 2200);
          });
        } else {
          /* Fallback for non-HTTPS or older browsers */
          try {
            var ta = document.createElement('textarea');
            ta.value = text;
            ta.style.cssText = 'position:fixed;opacity:0';
            document.body.appendChild(ta);
            ta.select();
            document.execCommand('copy');
            document.body.removeChild(ta);
            btn.innerHTML = '<i class="bi bi-check2" style="color:var(--success)"></i>';
            window.BooksyApp && window.BooksyApp.toast('Copied: ' + text, 'success', 2200);
            setTimeout(function() { btn.innerHTML = '<i class="bi bi-clipboard"></i>'; }, 2200);
          } catch (_) {
            window.BooksyApp && window.BooksyApp.toast('Copy not supported', 'error', 2200);
          }
        }
      });

      item.appendChild(btn);
    });
  }

  /* ── Search filter ── */
  var searchInput = document.getElementById('search');
  if (!searchInput) return;

  /* Remember which groups were originally open */
  var originalOpen = new Set();
  document.querySelectorAll('.endpoint-group.open').forEach(function(g) {
    originalOpen.add(g);
  });

  searchInput.addEventListener('input', function() {
    var q = searchInput.value.toLowerCase().trim();

    document.querySelectorAll('.endpoint-item').forEach(function(item) {
      var text = item.textContent.toLowerCase();
      item.style.display = (!q || text.includes(q)) ? '' : 'none';
    });

    document.querySelectorAll('.endpoint-group').forEach(function(group) {
      var visible = Array.from(group.querySelectorAll('.endpoint-item'))
        .some(function(i) { return i.style.display !== 'none'; });

      group.style.display = visible ? '' : 'none';

      if (q) {
        /* While searching: auto-expand groups that have results */
        if (visible) group.classList.add('open');
      } else {
        /* When search cleared: restore original open state */
        if (originalOpen.has(group)) {
          group.classList.add('open');
        } else {
          group.classList.remove('open');
        }
      }
    });
  });

  /* ── Accordion toggle ── */
  document.querySelectorAll('.endpoint-group-header').forEach(function(header) {
    header.addEventListener('click', function() {
      var group = header.closest('.endpoint-group');
      group.classList.toggle('open');
    });
  });

  /* ── Endpoint item keyboard shortcut (press 'c' to copy hovered) ── */
  addCopyButtons();
})();
