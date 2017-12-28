var require =
{
    baseUrl: '/Scripts/dist',
    paths: {
        domReady: "../domReady",
        lodash: "../lodash",
        moment: "../moment"
    },
    config: {
        moment: {
            noGlobal: true
        }
    },
    urlArgs: "bust=" + (new Date()).getTime()
};
