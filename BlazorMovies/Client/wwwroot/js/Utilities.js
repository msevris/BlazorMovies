function my_function(message) {
    console.log("from Utilities " + message)
}

function dotnetStaticInvocation() {
    DotNet.invokeMethodAsync("BlazorMovies.Client", "GetCurrentCount")
        .then(result => {
            console.log("count from javascript " + result);
        });
}

function dotnetInstanceInvokation(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}