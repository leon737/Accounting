define(["settings"], function(settings) {
    return {
        out: function () {
            if (!settings.debug) return;
			var args = Array.prototype.slice.call(arguments) || [];
            try {
                window.console.log.apply(window.console, args);
            } catch (err) {
                return;
            }
        }
    };
});