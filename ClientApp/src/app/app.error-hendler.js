"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppErrorHandler = void 0;
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler() {
    }
    AppErrorHandler.prototype.handleError = function (error) {
        //Sentry.captureException(error.originalError || error);
        console.log("ERROR");
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app.error-hendler.js.map