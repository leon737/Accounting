define(function () {
    const api = "/api";
    return {
        "chart" : {
            "api" : `${api}/chart`
        },
        "currency" : {
            "api" : `${api}/currency`
        },
        "account" : {
            "api" : chartId => `${api}/account/${chartId}`
        }
    };
});