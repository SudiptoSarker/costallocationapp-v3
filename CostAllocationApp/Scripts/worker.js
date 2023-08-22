var frequency = null
var tabs = 0;
self.onmessage = function (msg) {
    if (msg.data === 'new') {
        tabs++;
        console.log(tabs);
        //enableTimeout()
    } else if (msg.data === 'disableTimeout') {
        if (frequency !== null) {
            console.log("Timeout disabled")
            clearTimeout(frequency)
            frequency = null
        }
    }
}
function enableTimeout() {
    console.log("Timeout enabled")
    frequency = setTimeout(function () {
        postMessage('logout')
    }, 60 * 60 * 1000) // 1 Hour timeout
}