/**
 * Do a jump to a specific anchor
 * @param {string} anchorId Anchor id
 */
function jump(anchorId) {
    // Get the final location of the scrool
    var top = document.getElementById(anchorId).offsetTop;
    var left = document.getElementById(anchorId).offsetLeft;

    /**
     * Scroll to the specific location
     * @param {any} vals The final location
     */
    var callBack = function(vals) {
        console.log(arguments);
        window.scrollTo(vals[0], vals[1]);
    }

    var animator = createAnimator({
        // Initial location
        start: [0, 0],
        // Final location
        end: [left, top],
        duration: 400
    }, callBack);

    // Run the animation
    animator();
}

/**
 * Create an animator
 * @param {any} config
 * @param {any} callback
 * @param {any} done
 */
function createAnimator(config, callback, done) {
    if (typeof config !== "object") throw new TypeError("Arguement config expect an Object");

    var start = config.start;
    var mid = $.extend({}, start);      // Clone
    var math = $.extend({}, start);     // Precalculate the math
    var end = config.end;
    var duration = config.duration || 1000;
    var startTime;
    var endTime;

    // t * (b-d) / (a-c) + (a * d - b * c) / (a - c);
    function precalculate(a, b, c, d) {
        return [(b - d) / (a - c), (a * d - b * c) / (a - c)];
    }

    function calculate(key, t) {
        return t * math[key][0] + math[key][1];
    }

    function step() {
        var t = Date.now();
        var val = end;

        // Go to the next location until the timeout is done
        if (t < endTime) {
            val = mid;

            for (var key in mid) {
                mid[key] = calculate(key, t);
            }

            // Go to the next location
            callback(val);
            requestAnimationFrame(step);
        }
        // The timeout is done
        else {
            callback(val);
            done && done();
        }
    }

    // Run the animation
    return function () {
        startTime = Date.now();
        endTime = startTime + duration;

        for (var key in math) {
            math[key] = precalculate(startTime, start[key], endTime, end[key]);
        }

        step();
    }
}
