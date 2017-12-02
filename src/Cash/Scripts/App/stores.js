define(function() {
    return {
        createApiStore: function(url) {
            return DevExpress.data.AspNet.createStore({
                key: "id",
                loadUrl: url,
                updateUrl: url,
                insertUrl: url,
                deleteUrl: url
            });
        },
        createArrayStore: function(value) {
            return {
                store: {
                    type: 'array',
                    data: value,
                    key: "id"
                }
            };
        }
    };
});