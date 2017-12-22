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
            "api" : chartId => `${api}/account/${chartId}`,
            "active" : chartId => `${api}/account/active/${chartId}`,
        },
        "transaction" : {
            "create": `${api}/transaction/create`,
            "api": accountId => `${api}/transaction/${accountId}`,
            "extra": accountId => `${api}/transaction/extra/${accountId}`,
            "accountCard": accountId => `${api}/transaction/accountcard/${accountId}`
        }
    };
});