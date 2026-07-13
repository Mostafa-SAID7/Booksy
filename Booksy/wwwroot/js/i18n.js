/* =====================================================
   Booksy — Internationalization (i18n) Engine
   =====================================================
   Supports: en (English, LTR) | ar (Arabic, RTL)
   Storage:  localStorage key "booksyLang"
   Protocol: sends Accept-Language header on API calls
   Usage:    add data-i18n="section.key" to any element
*/

(function BooksyI18n() {
  'use strict';

  var STORAGE_KEY  = 'booksyLang';
  var SUPPORTED    = ['en', 'ar'];
  var RTL_LANGS    = ['ar'];
  var translations = {};

  /* ── Resolve nested key "nav.home" → obj.nav.home ── */
  function resolve(obj, key) {
    return key.split('.').reduce(function(o, k) {
      return o && o[k] !== undefined ? o[k] : null;
    }, obj);
  }

  /* ── Apply translations to DOM ── */
  function applyTranslations() {
    document.querySelectorAll('[data-i18n]').forEach(function(el) {
      var key = el.getAttribute('data-i18n');
      var val = resolve(translations, key);
      if (val) el.textContent = val;
    });

    document.querySelectorAll('[data-i18n-placeholder]').forEach(function(el) {
      var key = el.getAttribute('data-i18n-placeholder');
      var val = resolve(translations, key);
      if (val) el.placeholder = val;
    });

    document.querySelectorAll('[data-i18n-title]').forEach(function(el) {
      var key = el.getAttribute('data-i18n-title');
      var val = resolve(translations, key);
      if (val) el.title = val;
    });
  }

  /* ── Set document direction + lang ── */
  function applyDirection(lang) {
    var isRtl = RTL_LANGS.indexOf(lang) !== -1;
    document.documentElement.setAttribute('dir', isRtl ? 'rtl' : 'ltr');
    document.documentElement.setAttribute('lang', lang);
  }

  /* ── Patch BooksyApp.API to send Accept-Language ── */
  function patchApi(lang) {
    if (!window.BooksyApp || !window.BooksyApp.API) return;
    var api = window.BooksyApp.API;

    api._lang = lang;

    var origGet  = api.get.bind(api);
    var origPost = api.post.bind(api);

    api.get = function(path) {
      return fetch(api.base + path, {
        headers: { 'Accept': 'application/json', 'Accept-Language': api._lang }
      }).then(function(res) {
        if (!res.ok) throw new Error(res.status + ' ' + res.statusText);
        return res.json();
      });
    };

    api.post = function(path, data) {
      return fetch(api.base + path, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Accept-Language': api._lang
        },
        body: JSON.stringify(data)
      }).then(function(res) {
        if (!res.ok) throw new Error(res.status + ' ' + res.statusText);
        return res.json();
      });
    };
  }

  /* ── Inject language switcher into nav ── */
  function injectSwitcher(activeLang) {
    var cta = document.querySelector('.nav-cta');
    if (!cta || document.getElementById('lang-switcher')) return;

    var sw = document.createElement('div');
    sw.id = 'lang-switcher';
    sw.className = 'lang-switcher';
    sw.setAttribute('role', 'group');
    sw.setAttribute('aria-label', 'Language');

    SUPPORTED.forEach(function(lang) {
      var btn = document.createElement('button');
      btn.className = 'lang-btn' + (lang === activeLang ? ' active' : '');
      btn.textContent = lang.toUpperCase();
      btn.setAttribute('aria-pressed', String(lang === activeLang));
      btn.setAttribute('lang', lang);

      btn.addEventListener('click', function() {
        if (lang !== getCurrentLang()) {
          setLang(lang);
        }
      });

      sw.appendChild(btn);
    });

    cta.insertBefore(sw, cta.firstChild);
  }

  /* ── Get current stored language ── */
  function getCurrentLang() {
    try {
      var stored = localStorage.getItem(STORAGE_KEY);
      if (stored && SUPPORTED.indexOf(stored) !== -1) return stored;
    } catch (_) {}

    /* Detect from browser */
    var browser = (navigator.language || 'en').split('-')[0];
    return SUPPORTED.indexOf(browser) !== -1 ? browser : 'en';
  }

  /* ── Switch language ── */
  function setLang(lang) {
    try { localStorage.setItem(STORAGE_KEY, lang); } catch (_) {}
    loadAndApply(lang);
  }

  /* ── Load translation JSON and apply ── */
  function loadAndApply(lang) {
    fetch('/i18n/' + lang + '.json?v=1')
      .then(function(r) { return r.json(); })
      .then(function(data) {
        translations = data;
        applyDirection(lang);
        applyTranslations();
        patchApi(lang);
        injectSwitcher(lang);
        updateSwitcherState(lang);
      })
      .catch(function(err) {
        console.warn('[i18n] Failed to load ' + lang + '.json:', err);
      });
  }

  /* ── Update switcher buttons visual state ── */
  function updateSwitcherState(lang) {
    var sw = document.getElementById('lang-switcher');
    if (!sw) return;
    sw.querySelectorAll('.lang-btn').forEach(function(btn) {
      var active = btn.getAttribute('lang') === lang;
      btn.classList.toggle('active', active);
      btn.setAttribute('aria-pressed', String(active));
    });
  }

  /* ── Init ── */
  function init() {
    var lang = getCurrentLang();
    loadAndApply(lang);
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
  } else {
    init();
  }

  /* ── Expose for external use ── */
  window.BooksyI18n = {
    t: function(key) { return resolve(translations, key) || key; },
    setLang: setLang,
    getLang: getCurrentLang
  };
})();
