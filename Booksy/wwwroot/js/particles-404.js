/* Booksy 404 — canvas particle animation */
(function initParticles() {
    var canvas = document.getElementById('particles-canvas');
    if (!canvas) return;
    var ctx = canvas.getContext('2d');
    var W, H, particles = [];

    var COUNT   = 70;
    var MAX_DIST = 130;
    var PRIMARY  = '0,120,212';
    var ACCENT   = '80,230,255';

    function resize() {
        W = canvas.width  = window.innerWidth;
        H = canvas.height = window.innerHeight;
    }

    function randomParticle() {
        return {
            x: Math.random() * W,
            y: Math.random() * H,
            vx: (Math.random() - 0.5) * 0.5,
            vy: (Math.random() - 0.5) * 0.5,
            r: Math.random() * 2 + 0.8,
            color: Math.random() > 0.5 ? PRIMARY : ACCENT,
            alpha: Math.random() * 0.5 + 0.2
        };
    }

    function init() {
        resize();
        particles = [];
        for (var i = 0; i < COUNT; i++) {
            particles.push(randomParticle());
        }
    }

    function draw() {
        ctx.clearRect(0, 0, W, H);

        /* connections */
        for (var i = 0; i < particles.length; i++) {
            for (var j = i + 1; j < particles.length; j++) {
                var a = particles[i], b = particles[j];
                var dx = a.x - b.x, dy = a.y - b.y;
                var dist = Math.sqrt(dx * dx + dy * dy);
                if (dist < MAX_DIST) {
                    var opacity = (1 - dist / MAX_DIST) * 0.2;
                    ctx.beginPath();
                    ctx.strokeStyle = 'rgba(' + a.color + ',' + opacity + ')';
                    ctx.lineWidth   = 0.6;
                    ctx.moveTo(a.x, a.y);
                    ctx.lineTo(b.x, b.y);
                    ctx.stroke();
                }
            }
        }

        /* dots + movement */
        particles.forEach(function(p) {
            ctx.beginPath();
            ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2);
            ctx.fillStyle = 'rgba(' + p.color + ',' + p.alpha + ')';
            ctx.fill();

            p.x += p.vx;
            p.y += p.vy;
            if (p.x < -10) p.x = W + 10;
            if (p.x > W + 10) p.x = -10;
            if (p.y < -10) p.y = H + 10;
            if (p.y > H + 10) p.y = -10;
        });

        requestAnimationFrame(draw);
    }

    init();
    draw();
    window.addEventListener('resize', resize, { passive: true });
})();
